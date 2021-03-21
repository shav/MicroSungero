using System;
using System.Linq;
using System.Threading.Tasks;
using MicroSungero.Data.Exceptions;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Data
{
  /// <summary>
  /// Proxy class that pretends to be unit-of-work implementation.
  /// It either uses current unit-of-work instance if exists, or create new temporary unit-of-work.
  /// </summary>
  internal class UnitOfWorkProvider : IUnitOfWork, IDisposable
  {
    #region IUnitOfWork

    public TRecord Create<TRecord>() where TRecord : class
    {
      this.CheckIfNotDisposed(nameof(Create));
      return this.unitOfWork.Create<TRecord>();
    }

    public TRecord Attach<TRecord>(TRecord record) where TRecord : class
    {
      this.CheckIfNotDisposed(nameof(Attach));
      return this.unitOfWork.Attach(record);
    }

    public void Delete<TRecord>(TRecord record) where TRecord : class
    {
      this.CheckIfNotDisposed(nameof(Delete));
      this.unitOfWork.Delete(record);
    }

    public IQueryable<TRecord> GetAll<TRecord>() where TRecord : class
    {
      this.CheckIfNotDisposed(nameof(GetAll));
      return this.unitOfWork.GetAll<TRecord>();
    }

    public TEntity GetById<TEntity>(int id) where TEntity: class, IEntity
    {
      this.CheckIfNotDisposed(nameof(GetById));
      return this.unitOfWork.GetById<TEntity>(id);
    }

    public Task SubmitChanges()
    {
      this.CheckIfNotDisposed(nameof(SubmitChanges));
      return this.unitOfWork.SubmitChanges();
    }

    #endregion

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

    #region Methods

    /// <summary>
    /// Check before executing action if the unit-of-work proxy has not been disposed before.
    /// If the unit-of-work proxy is disposed then throws exception.
    /// </summary>
    /// <param name="actionName">[Optional] Action name.</param>
    private void CheckIfNotDisposed(string actionName = default)
    {
      if (this.disposed)
        throw new InvalidOperationException($"Cannot perform action {actionName} because the current {nameof(UnitOfWorkProvider)} is disposed.");
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create proxy for unit-of-work.
    /// </summary>
    /// <param name="unitOfWorkContext">Unit-of-work context.</param>
    public UnitOfWorkProvider(IUnitOfWorkContext unitOfWorkContext)
    {
      if (unitOfWorkContext == null)
        throw new UnitOfWorkException($"Cannot get active {nameof(IUnitOfWork)}: {nameof(unitOfWorkContext)} is not assigned");

      if (unitOfWorkContext.CurrentUnitOfWork != null)
      {
        this.unitOfWork = unitOfWorkContext.CurrentUnitOfWork;
        this.isCurrent = true;
      }
      else
      {
        if (unitOfWorkContext.Factory == null)
          throw new UnitOfWorkException($"Cannot create new {nameof(IUnitOfWork)}: {nameof(unitOfWorkContext.Factory)} is not assigned");

        this.unitOfWork = unitOfWorkContext.Factory.Create();
        this.isCurrent = false;

        if (this.unitOfWork == null)
          throw new UnitOfWorkException($"Cannot use {nameof(UnitOfWorkProvider)}: {nameof(unitOfWorkContext.Factory)} returned null value of {nameof(IUnitOfWork)}");
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

    ~UnitOfWorkProvider()
    {
      this.Dispose(false);
    }

    #endregion
  }
}
