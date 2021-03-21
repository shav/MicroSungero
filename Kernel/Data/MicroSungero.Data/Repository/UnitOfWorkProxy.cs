using System;
using System.Linq;
using System.Threading.Tasks;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Data
{
  /// <summary>
  /// Proxy class that pretends to be unit-of-work implementation.
  /// It either uses current unit-of-work instance if exists, or create new temporary unit-of-work.
  /// </summary>
  internal class UnitOfWorkProxy : IUnitOfWork, IDisposable
  {
    #region Properties and fields

    /// <summary>
    /// Proxied unit-of-work instance.
    /// </summary>
    private IUnitOfWork unitOfWork;

    /// <summary>
    /// Indicates that active existing unit-of-work was proxied by this provider.
    /// </summary>
    private bool isCurrent;

    #endregion

    #region IUnitOfWork

    public TRecord Create<TRecord>() where TRecord : class
    {
      return this.unitOfWork.Create<TRecord>();
    }

    public TRecord Attach<TRecord>(TRecord record) where TRecord : class
    {
      return this.unitOfWork.Attach(record);
    }

    public void Delete<TRecord>(TRecord record) where TRecord : class
    {
      this.unitOfWork.Delete(record);
    }

    public IQueryable<TRecord> GetAll<TRecord>() where TRecord : class
    {
      return this.unitOfWork.GetAll<TRecord>();
    }

    public TEntity GetById<TEntity>(int id) where TEntity: class, IEntity
    {
      return this.unitOfWork.GetById<TEntity>(id);
    }

    public Task SubmitChanges()
    {
      return this.unitOfWork.SubmitChanges();
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create proxy for unit-of-work.
    /// </summary>
    /// <param name="unitOfWorkContext">Unit-of-work context.</param>
    public UnitOfWorkProxy(IUnitOfWorkContext unitOfWorkContext)
    {
      if (unitOfWorkContext.CurrentUnitOfWork != null)
      {
        this.unitOfWork = unitOfWorkContext.CurrentUnitOfWork;
        this.isCurrent = true;
      }
      else
      {
        this.unitOfWork = unitOfWorkContext.Factory.Create();
        this.isCurrent = false;
      }
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
        if (!this.isCurrent)
          (this.unitOfWork as IDisposable)?.Dispose();
      }
      this.disposed = true;
    }

    ~UnitOfWorkProxy()
    {
      this.Dispose(false);
    }

    #endregion
  }
}
