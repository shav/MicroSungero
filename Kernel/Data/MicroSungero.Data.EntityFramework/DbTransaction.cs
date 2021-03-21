using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace MicroSungero.Data.EntityFramework
{
  /// <summary>
  /// Database transaction implementation through EntityFramework.
  /// </summary>
  public class DbTransaction : ITransaction
  {
    #region ITransaction

    public Guid TransactionId => this.transaction.TransactionId;

    public void Commit()
    {
      this.transaction.Commit();
    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
      return this.transaction.CommitAsync(cancellationToken);
    }

    public void Dispose()
    {
      this.transaction.Dispose();
    }

    public void Rollback()
    {
      this.transaction.Rollback();
    }

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
      return this.transaction.RollbackAsync(cancellationToken);
    }

    #endregion

    #region Properties and fields

    /// <summary>
    /// Original EntityFramework transaction.
    /// </summary>
    private IDbContextTransaction transaction;

    #endregion

    #region Constructors

    /// <summary>
    /// Wrap EntityFramework transaction instance.
    /// </summary>
    /// <param name="transaction">Original EntityFramework transaction instamce.</param>
    public DbTransaction(IDbContextTransaction transaction)
    {
      this.transaction = transaction;
    }

    #endregion
  }
}
