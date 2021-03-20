using System;
using System.Linq;
using System.Threading.Tasks;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Data
{
  /// <summary>
  /// Implementation of "unit of work" pattern.
  /// The scope that maintains and tracks changes of a list of objects affected by a business transaction.
  /// </summary>
  public class Session : IUnitOfWork, IDisposable
  {
    #region IUnitOfWork

    public TRecord Create<TRecord>() where TRecord : class
    {
      throw new NotImplementedException();
    }

    public TRecord Attach<TRecord>(TRecord record) where TRecord : class
    {
      throw new NotImplementedException();
    }

    public void Delete<TRecord>(TRecord record) where TRecord : class
    {
      throw new NotImplementedException();
    }

    public IQueryable<TRecord> GetAll<TRecord>() where TRecord : class
    {
      throw new NotImplementedException();
    }

    public TEntity GetById<TEntity>(int id) where TEntity : class, IEntity
    {
      throw new NotImplementedException();
    }

    public Task SubmitChanges()
    {
      throw new NotImplementedException();
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
        // TODO
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
