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
  public class UnitOfWork : IUnitOfWork, IDisposable
  {
    #region Properties and fields

    /// <summary>
    /// Current active unit-of-work.
    /// </summary>
    /// <remarks>Returns the most inner unit-of-work within units stack.</remarks>
    public static UnitOfWork Current => UnitsOfWorkStack.LastOrDefault();

    private static readonly AsyncLocal<ICollection<UnitOfWork>> unitsOfWorkStack = new AsyncLocal<ICollection<UnitOfWork>>();

    /// <summary>
    /// Units-of-work stack.
    /// </summary>
    /// <remarks>
    /// Unit-of-work can be implicitly wrapped by other outer unit-of-work 
    /// (beacuse when creating new unit-of-work you don't know and don't care about there is outer unit-of-work or not).
    /// All wrapped units stack is treated as single unit of work.
    /// It means that all changes from inner units-of-work actually will be submitted only on submitting changes of the most outer unit-of-work as single transaction,
    /// but not when calling the method <see cref="SubmitChanges"/> of the inner unit.
    /// </remarks>
    private static ICollection<UnitOfWork> UnitsOfWorkStack
    {
      get 
      { 
        return unitsOfWorkStack.Value ?? (unitsOfWorkStack.Value = new Collection<UnitOfWork>());
      }
      set 
      { 
        unitsOfWorkStack.Value = value;
      }
    }

    /// <summary>
    /// Data access context.
    /// </summary>
    private IDbContext dbContext;

    /// <summary>
    /// Database context factory.
    /// </summary>
    private IDbContextFactory dbContextFactory;

    /// <summary>
    /// Indicates that the unit-of-work is submitting changes at this moment.
    /// </summary>
    private bool isActiveSubmit = false;

    #endregion

    #region IUnitOfWork

    public TRecord Create<TRecord>() where TRecord : class
    {
      this.CheckIfNotDisposed(nameof(Create));

      var record = Activator.CreateInstance<TRecord>();
      var persistentRecord = record as IPersistentObject;
      if (persistentRecord != null)
        persistentRecord.IsTransient = true;

      this.Attach(record);

      return record;
    }

    public void Delete<TRecord>(TRecord record) where TRecord : class
    {
      this.CheckIfNotDisposed(nameof(Delete));

      this.dbContext.Remove(record);

      var persistentRecord = record as IPersistentObject;
      if (persistentRecord != null)
        persistentRecord.IsDeleted = true;
    }

    public TRecord Attach<TRecord>(TRecord record) where TRecord : class
    {
      this.CheckIfNotDisposed(nameof(Attach));

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
          throw new UnitOfWorkException($"Transient object {persistentRecord} cannot be attached to the {nameof(UnitOfWork)}, because it has already been attached in {entry.State} state before");
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
      this.CheckIfNotDisposed(nameof(GetAll));
      return this.dbContext.GetAll<TRecord>();
    }

    public TEntity GetById<TEntity>(int id) where TEntity : class, IEntity
    {
      this.CheckIfNotDisposed(nameof(GetById));

      var entity = this.dbContext.GetById<TEntity>(id);
      if (entity == null)
        throw new ObjectNotFoundException(typeof(TEntity).FullName, $"Id = {id}");

      return entity;
    }

    public async Task SubmitChanges()
    {
      this.CheckIfNotDisposed(nameof(SubmitChanges));

      // If there are outer unit-of-work then we should submit changes only at the most outer unit-of-work.
      if (UnitsOfWorkStack.Count > 1)
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
        throw new UnitOfWorkException($"{nameof(UnitOfWork)} is already submitting.");

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
    /// Execute action with tracking persistent status of persistent objects attached to unit-of-work.
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

    /// <summary>
    /// Check before executing action if the unit-of-work has not been disposed before.
    /// If the unit-of-work is disposed then throws exception.
    /// </summary>
    /// <param name="actionName">[Optional] Action name.</param>
    private void CheckIfNotDisposed(string actionName = default)
    {
      if (this.disposed)
        throw new InvalidOperationException($"Cannot perform action {actionName} because the current {nameof(UnitOfWork)} is disposed.");
    }

    /// <summary>
    /// Create new database access context.
    /// </summary>
    /// <returns>Database access context.</returns>
    private IDbContext CreateNewDbContext()
    {
      if (this.dbContextFactory == null)
        throw new UnitOfWorkException($"Cannot create new {nameof(IDbContext)}: {nameof(dbContextFactory)} is not assigned");

      return this.dbContextFactory.Create();
    }

    /// <summary>
    /// Set database context implementation.
    /// </summary>
    /// <param name="dbContext">Database context.</param>
    private void SetDatabaseContext(IDbContext dbContext)
    {
      if (dbContext != null)
      {
        this.dbContext = dbContext;
      }
      else
      {
        this.dbContext = UnitsOfWorkStack.LastOrDefault()?.dbContext ?? CreateNewDbContext();
      }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create new unit-of-work.
    /// </summary>
    /// <param name="dbContextFactory">Database context factory.</param>
    /// <param name="dbContext">Database access context.</param>
    public UnitOfWork(IDbContextFactory dbContextFactory, IDbContext dbContext)
    {
      this.dbContextFactory = dbContextFactory;
      this.SetDatabaseContext(dbContext);
      UnitsOfWorkStack.Add(this);
    }

    /// <summary>
    /// Create new unit-of-work.
    /// </summary>
    /// <param name="dbContextFactory">Database context factory.</param>
    public UnitOfWork(IDbContextFactory dbContextFactory)
      : this(dbContextFactory, null)
    {
    }

    /// <summary>
    /// Create new unit-of-work.
    /// </summary>
    /// <param name="dbContext">Database access context.</param>
    public UnitOfWork(IDbContext dbContext)
      : this(null, dbContext)
    {
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
        UnitsOfWorkStack.Remove(this);
      }
      this.disposed = true;
    }

    ~UnitOfWork()
    {
      this.Dispose(false);
    }

    #endregion
  }
}
