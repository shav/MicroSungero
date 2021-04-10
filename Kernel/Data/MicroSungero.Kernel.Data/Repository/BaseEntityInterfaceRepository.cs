using System;
using System.Linq;
using System.Linq.Expressions;
using MicroSungero.Common.Utils;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Data
{
  /// <summary>
  /// Base implementation of repository that is used for accessing at storage to entities implementing specified interface.
  /// </summary>
  /// <typeparam name="TEntity">Type of entities at storage.</typeparam>
  /// <typeparam name="TEntityInterface">Entity interface that is implemented by entities.</typeparam>
  public abstract class BaseEntityInterfaceRepository<TEntity, TEntityInterface> : BaseEntityRepository<TEntity>, IEntityRepository<TEntityInterface>
   where TEntity : class, TEntityInterface
   where TEntityInterface : IEntity
  {
    #region IEntityRepository

    TEntityInterface IRepository<TEntityInterface>.Get(Expression<Func<TEntityInterface, bool>> filter)
    {
      return this.Get(filter.ChangeParameterType<TEntityInterface, bool, TEntity>());
    }

    IQueryable<TEntityInterface> IRepository<TEntityInterface>.GetAll()
    {
      return this.GetAll();
    }

    IQueryable<TEntityInterface> IRepository<TEntityInterface>.GetAll(Expression<Func<TEntityInterface, bool>> filter)
    {
      return this.GetAll(filter.ChangeParameterType<TEntityInterface, bool, TEntity>());
    }

    TEntityInterface IEntityRepository<TEntityInterface>.GetById(int id)
    {
      return this.GetById(id);
    }

    TEntityInterface IRepository<TEntityInterface>.Create()
    {
      return this.Create();
    }

    void IRepository<TEntityInterface>.Update(TEntityInterface entity)
    {
      this.Update((TEntity)entity);
    }

    void IRepository<TEntityInterface>.Delete(TEntityInterface entity)
    {
      this.Delete((TEntity)entity);
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create entity repository.
    /// </summary>
    /// <param name="unitOfWorkContext">Unit-of-work context.</param>
    public BaseEntityInterfaceRepository(IUnitOfWorkContext unitOfWorkContext)
      : base(unitOfWorkContext)
    {
    }

    #endregion
  }
}
