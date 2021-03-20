using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MicroSungero.Data.Exceptions;
using MicroSungero.Kernel.Domain;
using MicroSungero.Kernel.Domain.Entities;
using MicroSungero.Kernel.Domain.Exceptions;

namespace MicroSungero.Data
{
  /// <summary>
  /// Implementation of "unit of work" pattern.
  /// The scope that maintains and tracks changes of a list of objects affected by a business transaction.
  /// </summary>
  public class Session : IUnitOfWork, IDisposable
  {
    #region Properties and fields

    /// <summary>
    /// Current active session.
    /// </summary>
    /// <remarks>Returns the most inner session within session stack.</remarks>
    public static Session Current => SessionStack.LastOrDefault();

    private static readonly AsyncLocal<ICollection<Session>> sessionStack = new AsyncLocal<ICollection<Session>>();

    /// <summary>
    /// Session stack.
    /// </summary>
    /// <remarks>
    /// Session can be implicitly wrapped by other outer session 
    /// (beacuse when creating new session you don't know and shouldn't care about there is outer session or not).
    /// All wrapped sessions stack is treated as single unit of work.
    /// It means that all changes from inner sessions actually will be submitted only on submitting changes of the most outer session as single transaction,
    /// but not when calling the method <see cref="SubmitChanges"/> of the inner session.
    /// </remarks>
    private static ICollection<Session> SessionStack
    {
      get 
      { 
        return sessionStack.Value ?? (sessionStack.Value = new Collection<Session>());
      }
      set 
      { 
        sessionStack.Value = value;
      }
    }

    /// <summary>
    /// Data access context.
    /// </summary>
    private IDbContext dbContext;

    /// <summary>
    /// Indicates that the session is submitting changes at this moment.
    /// </summary>
    private bool isActiveSubmit = false;

    #endregion

    #region IUnitOfWork

    public TRecord Create<TRecord>() where TRecord : class
    {
      var record = Activator.CreateInstance<TRecord>();
      var persistentRecord = record as IPersistentObject;
      if (persistentRecord != null)
        persistentRecord.IsTransient = true;

      this.Attach(record);

      return record;
    }

    public void Delete<TRecord>(TRecord record) where TRecord : class
    {
      this.dbContext.Remove(record);

      var persistentRecord = record as IPersistentObject;
      if (persistentRecord != null)
        persistentRecord.IsDeleted = true;
    }

    public TRecord Attach<TRecord>(TRecord record) where TRecord : class
    {
      var persistentRecord = record as IPersistentObject;
      if (persistentRecord == null)
        return this.dbContext.Attach(record).Record;

      if (persistentRecord.IsDeleted && persistentRecord.IsTransient)
      {
        // It is phantom (new entity has been already deleted but hasn't been saved to storage yet)
        return record;
      }

      var entry = this.dbContext.GetTrackingEntry(persistentRecord);
      if (persistentRecord.IsTransient)
      {
        if (entry.State == RecordState.Detached)
        {
          entry = this.dbContext.Add(persistentRecord);
          entry.State = RecordState.Added;
        }
        else if (entry.State != RecordState.Added && entry.State != RecordState.Modified)
          throw new SessionException($"Transient object {persistentRecord} cannot be attached to session, because it has already attached to session in {entry.State} state");
      }
      else if (persistentRecord.IsDeleted)
      {
        entry = this.dbContext.Remove(persistentRecord);
        entry.State = RecordState.Deleted;
      }
      else
      {
        entry = this.dbContext.Update(persistentRecord);
        entry.State = RecordState.Modified;
      }
      return (TRecord)entry.Record;
    }

    public IQueryable<TRecord> GetAll<TRecord>() where TRecord : class
    {
      return this.dbContext.GetAll<TRecord>();
    }

    public TEntity GetById<TEntity>(int id) where TEntity : class, IEntity
    {
      var entity = this.dbContext.GetById<TEntity>(id);
      if (entity == null)
        throw new ObjectNotFoundException(typeof(TEntity).FullName, $"Id = {id}");

      return entity;
    }

    public async Task SubmitChanges()
    {
      // If there are outer sessions then we should submit changes only at the most outer session.
      if (SessionStack.Count > 1)
        return;

      this.BeginSubmit();
      try
      {
        var transaction = await this.dbContext.BeginTransactionAsync();
        await this.SaveChangesAsync();
        this.dbContext.CommitTransaction(transaction);
      }
      catch
      {
        this.dbContext.RollbackTransaction();
        throw;
      }
      finally
      {
        this.EndSubmit();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Start submitting of tracking changes.
    /// </summary>
    private void BeginSubmit()
    {
      if (this.isActiveSubmit)
        throw new SessionException("Session is already submitting.");

      this.isActiveSubmit = true;
    }

    /// <summary>
    /// End submitting of tracking changes.
    /// </summary>
    private void EndSubmit()
    {
      this.isActiveSubmit = false;
    }

    /// <summary>
    /// Save tracking entries changes to database within current transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of entries written to the database.</returns>
    private async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      int writtenEntriesCount = default;
      await this.WithTrackPersistentStatus(new Task(() =>
      {
        writtenEntriesCount = dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult();
      }));
      return writtenEntriesCount;
    }

    /// <summary>
    /// Ececute action with tracking persistent status of persistent objects attached to session.
    /// </summary>
    /// <param name="action">Action to execute.</param>
    private async Task WithTrackPersistentStatus(Task action)
    {
      var addedEntries = this.dbContext.GetTrackingEntries<IPersistentObject>()
        .Where(e => e.State == RecordState.Added || (e.Record as IPersistentObject)?.IsTransient == true)
        .Select(e => e.Record)
        .ToArray();

      var changedEntries = this.dbContext.GetTrackingEntries<IPersistentObject>()
        .Where(e => e.State == RecordState.Modified)
        .Select(e => e.Record)
        .ToArray();

      var deletedEntries = this.dbContext.GetTrackingEntries<IPersistentObject>()
        .Where(e => e.State == RecordState.Deleted || (e.Record as IPersistentObject)?.IsDeleted == true)
        .Select(e => e.Record)
        .ToArray();

      action.Start();
      await action;

      foreach (var addedEntry in addedEntries)
      {
        addedEntry.IsTransient = false;
      }
      foreach (var deletedEntry in deletedEntries)
      {
        deletedEntry.IsDeleted = true;
      }
    }

    #endregion

    #region Constructors

    public Session()
    {
      SessionStack.Add(this);
    }

    #endregion

    #region IDisposable

    private bool disposed = false;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;

      if (disposing)
      {
        SessionStack.Remove(this);
      }
      this.disposed = true;
    }

    ~Session()
    {
      this.Dispose(false);
    }

    #endregion
  }
}
