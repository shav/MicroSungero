using System;
using MicroSungero.Kernel.Data.Repositories;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Data
{
  /// <summary>
  /// Base implementation of repository that is used for accessing to entities at storage.
  /// </summary>
  /// <typeparam name="TEntity">Type of entities at storage.</typeparam>
  public abstract class BaseEntityRepository<TEntity> : BaseRepository<TEntity>, IEntityRepository<TEntity>
    where TEntity : class, IEntity
  {
    #region IEntityRepository

    public virtual TEntity GetById(int id)
    {
      using (var unitOfWork = new UnitOfWorkProvider(this.unitOfWorkContext))
      {
        return unitOfWork.GetById<TEntity>(id);
      }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create entity repository.
    /// </summary>
    /// <param name="unitOfWorkContext">Unit-of-work context.</param>
    public BaseEntityRepository(IUnitOfWorkContext unitOfWorkContext)
      : base(unitOfWorkContext)
    {
    }

    #endregion
  }
}
