using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroSungero.Data
{
  /// <summary>
  /// The database transaction.
  /// </summary>
  public interface ITransaction: IDisposable
  {
    /// <summary>
    /// The transaction identifier.
    /// </summary>
    Guid TransactionId { get; }

    /// <summary>
    /// Commits all changes made to the database in the current transaction.
    /// </summary>
    void Commit();

    /// <summary>
    /// Discards all changes made to the database in the current transaction.
    /// </summary>
    void Rollback();

    /// <summary>
    /// Commits all changes made to the database in the current transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Discards all changes made to the database in the current transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task RollbackAsync(CancellationToken cancellationToken = default);
  }
}
