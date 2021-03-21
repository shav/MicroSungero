using System;
using System.Linq;
using System.Linq.Expressions;
using MicroSungero.Data.Repositories;

namespace MicroSungero.Data
{
  /// <summary>
  /// Base repository implementation.
  /// </summary>
  /// <typeparam name="TRecord">Type of records at storage.</typeparam>
  public abstract class BaseRepository<TRecord> : IRepository<TRecord>
    where TRecord : class
  {
    #region Properties and fields

    /// <summary>
    /// Unit-of-work context.
    /// </summary>
    protected IUnitOfWorkContext unitOfWorkContext;

    #endregion

    #region IRepository

    public virtual TRecord Get(Expression<Func<TRecord, bool>> filter)
    {
      return this.GetAll(filter).SingleOrDefault();
    }

    public virtual IQueryable<TRecord> GetAll()
    {
      using (var unitOfWork = new UnitOfWorkProvider(this.unitOfWorkContext))
      {
        return unitOfWork.GetAll<TRecord>();
      }
    }

    public virtual IQueryable<TRecord> GetAll(Expression<Func<TRecord, bool>> filter)
    {
      return this.GetAll().Where(filter);
    }

    public virtual TRecord Create()
    {
      using (var unitOfWork = new UnitOfWorkProvider(this.unitOfWorkContext))
      {
        return unitOfWork.Create<TRecord>();
      }
    }

    public virtual void Update(TRecord record)
    {
      using (var unitOfWork = new UnitOfWorkProvider(this.unitOfWorkContext))
      {
        unitOfWork.Attach(record);
      }
    }

    public virtual void Delete(TRecord record)
    {
      using (var unitOfWork = new UnitOfWorkProvider(this.unitOfWorkContext))
      {
        unitOfWork.Delete(record);
      }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create repository.
    /// </summary>
    /// <param name="unitOfWorkContext">Unit-of-work context.</param>
    public BaseRepository(IUnitOfWorkContext unitOfWorkContext)
    {
      this.unitOfWorkContext = unitOfWorkContext;
    }

    #endregion
  }
}
