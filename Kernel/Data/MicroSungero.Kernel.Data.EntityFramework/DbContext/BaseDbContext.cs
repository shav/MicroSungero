using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Data.EntityFramework
{
  /// <summary>
  /// Base database access context implementation through EntityFramework.
  /// </summary>
  public abstract class BaseDbContext : DbContext, IDbContext
  {
    #region Constants

    /// <summary>
    /// Default transaction isolation level.
    /// </summary>
    public const IsolationLevel DEFAULT_TRANSACTION_ISOLATION_LEVEL = IsolationLevel.ReadCommitted;

    #endregion

    #region IDbContext

    public ITransaction CurrentTransaction { get; private set; }

    public bool HasActiveTransaction => this.CurrentTransaction != null;

    public async Task<ITransaction> BeginTransactionAsync()
    {
      if (this.CurrentTransaction != null)
        return null;

      return this.CurrentTransaction = new DbTransaction(await this.Database.BeginTransactionAsync(this.TransactionIsolationLevel));
    }

    public void CommitTransaction(ITransaction transaction)
    {
      if (transaction == null)
        throw new ArgumentNullException(nameof(transaction));

      if (transaction != this.CurrentTransaction)
        throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

      try
      {
        transaction.Commit();
      }
      catch
      {
        RollbackTransaction();
        throw;
      }
      finally
      {
        if (this.CurrentTransaction != null)
        {
          this.CurrentTransaction.Dispose();
          this.CurrentTransaction = null;
        }
      }
    }

    public void RollbackTransaction()
    {
      try
      {
        this.CurrentTransaction?.Rollback();
      }
      finally
      {
        if (this.CurrentTransaction != null)
        {
          this.CurrentTransaction.Dispose();
          this.CurrentTransaction = null;
        }
      }
    }

    public TEntity GetById<TEntity>(int id) where TEntity : class, IEntity
    {
      var entity = this.Set<TEntity>().SingleOrDefault(e => e.Id == id);
      if (entity == null)
      {
        entity = this.Set<TEntity>().Local.SingleOrDefault(e => e.Id == id);
      }
      return entity;
    }

    public IQueryable<TRecord> GetAll<TRecord>() where TRecord : class
    {
      return this.Set<TRecord>().AsQueryable();
    }

    public IRecordEntry<TRecord> GetTrackingEntry<TRecord>(TRecord entity) where TRecord : class
    {
      return new RecordEntry<TRecord>(this.Entry<TRecord>(entity));
    }

    public IEnumerable<IRecordEntry<TRecord>> GetTrackingEntries<TRecord>() where TRecord : class
    {
      return this.ChangeTracker.Entries<TRecord>()
        .Select(e => new RecordEntry<TRecord>(e))
        .ToList();
    }

    public new IRecordEntry<TRecord> Add<TRecord>(TRecord entity) where TRecord : class
    {
      var addedEntry = this.Set<TRecord>().Add(entity);
      return new RecordEntry<TRecord>(addedEntry);
    }

    public new IRecordEntry<TRecord> Update<TRecord>(TRecord entity) where TRecord : class
    {
      var updatedEntry = this.Set<TRecord>().Update(entity);
      return new RecordEntry<TRecord>(updatedEntry);
    }

    public new IRecordEntry<TRecord> Remove<TRecord>(TRecord entity) where TRecord : class
    {
      var removedEntry = this.Set<TRecord>().Remove(entity);
      return new RecordEntry<TRecord>(removedEntry);
    }

    public new IRecordEntry<TRecord> Attach<TRecord>(TRecord record) where TRecord : class
    {
      return new RecordEntry<TRecord>(base.Attach<TRecord>(record));
    }

    #endregion

    #region DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    #endregion

    #region Properties and fields

    /// <summary>
    /// Database connection settings.
    /// </summary>
    public IDatabaseConnectionSettings ConnectionSettings { get; private set; }

    /// <summary>
    /// Transaction isolation level.
    /// </summary>
    public IsolationLevel TransactionIsolationLevel => this.ConnectionSettings?.TransactionIsolationLevel ?? DEFAULT_TRANSACTION_ISOLATION_LEVEL;

    #endregion

    #region Constructors

    /// <summary>
    /// Create database context.
    /// </summary>
    /// <param name="options">Options.</param>
    /// <param name="connectionSettings">Database connection settings.</param>
    public BaseDbContext(DbContextOptions options, IDatabaseConnectionSettings connectionSettings = null)
      : base(options)
    {
      this.ConnectionSettings = connectionSettings;
    }

    #endregion
  }
}
