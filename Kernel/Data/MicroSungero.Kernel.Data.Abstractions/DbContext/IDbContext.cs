using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Data
{
  /// <summary>
  /// Database access context.
  /// </summary>
  public interface IDbContext
  {
    #region Properties

    /// <summary>
    /// Active database transaction.
    /// </summary>
    ITransaction CurrentTransaction { get; }

    /// <summary>
    /// Check if active transaction exists.
    /// </summary>
    bool HasActiveTransaction { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Begin transaction asynchronously.
    /// </summary>
    Task<ITransaction> BeginTransactionAsync();

    /// <summary>
    /// Commit transaction changes to database.
    /// </summary>
    /// <param name="transaction">Transaction.</param>
    void CommitTransaction(ITransaction transaction);

    /// <summary>
    /// Rollback transaction changes from database.
    /// </summary>
    void RollbackTransaction();

    /// <summary>
    /// Save tracking entries changes to database within current transaction.
    /// </summary>
    /// <returns>The number of entries written to the database.</returns>
    int SaveChanges();

    /// <summary>
    /// Save tracking entries changes to database within current transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Find entity by Id.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="id">Entity Id.</param>
    /// <returns>Entity with specified Id.</returns>
    TEntity GetById<TEntity>(int id) where TEntity : class, IEntity;

    /// <summary>
    /// Get all records of specified type as query.
    /// </summary>
    /// <typeparam name="TRecord">Type of records.</typeparam>
    /// <returns>Query for all records of type.</returns>
    IQueryable<TRecord> GetAll<TRecord>() where TRecord : class;

    /// <summary>
    /// Get change tracking entry for persistent record.
    /// </summary>
    /// <typeparam name="TRecord">Type of persistent record.</typeparam>
    /// <param name="record">Persistent record.</param>
    /// <returns>Change tracking entry.</returns>
    IRecordEntry<TRecord> GetTrackingEntry<TRecord>(TRecord record) where TRecord : class;

    /// <summary>
    /// Get all change tracking entries for persistent records of specified type.
    /// </summary>
    /// <typeparam name="TRecord">Type of persistent records.</typeparam>
    /// <returns>Collection of change tracking entries.</returns>
    IEnumerable<IRecordEntry<TRecord>> GetTrackingEntries<TRecord>() where TRecord : class;

    /// <summary>
    /// Add new persistent record to the change tracking context.
    /// </summary>
    /// <typeparam name="TRecord">Type of persistent record.</typeparam>
    /// <param name="record">Persistent record.</param>
    /// <returns>Change tracking entry for added persistent record.</returns>
    IRecordEntry<TRecord> Add<TRecord>(TRecord record) where TRecord : class;

    /// <summary>
    /// Update existing persistent record at the change tracking context.
    /// </summary>
    /// <typeparam name="TRecord">Type of persistent record.</typeparam>
    /// <param name="record">Persistent record.</param>
    /// <returns>Change tracking entry for updated persistent record.</returns>
    IRecordEntry<TRecord> Update<TRecord>(TRecord record) where TRecord : class;

    /// <summary>
    /// Remove persistent record from change tracking context.
    /// </summary>
    /// <typeparam name="TRecord">Type of persistent record.</typeparam>
    /// <param name="record">Persistent record.</param>
    /// <returns>Change tracking entry for removed persistent record.</returns>
    IRecordEntry<TRecord> Remove<TRecord>(TRecord record) where TRecord : class;

    /// <summary>
    /// Start tracking changes of the record by default (initially at unchanged state).
    /// </summary>
    /// <typeparam name="TRecord">Type of record.</typeparam>
    /// <param name="record">Record.</param>
    /// <returns>Change tracking entry for the record.</returns>
    IRecordEntry<TRecord> Attach<TRecord>(TRecord record) where TRecord : class;

    #endregion
  }
}
