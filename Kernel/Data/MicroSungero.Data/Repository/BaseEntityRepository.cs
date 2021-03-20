using System;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Data
{
  /// <summary>
  /// Base implementation of repository that is used for accessing to entities at storage.
  /// </summary>
  /// <typeparam name="TEntity">Type of entities at storage.</typeparam>
  public abstract class BaseEntityRepository<TEntity> : BaseRepository<TEntity>, IEntityRepository<TEntity>
    where TEntity : class, IEntity
  {
    #region IEntityRepository

    public TEntity GetById(int id)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
