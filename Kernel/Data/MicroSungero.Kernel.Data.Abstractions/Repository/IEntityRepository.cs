using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Data
{
  /// <summary>
  /// Repository that is used for accessing to entities at storage.
  /// </summary>
  /// <typeparam name="TEntity">Type of entities at storage.</typeparam>
  public interface IEntityRepository<TEntity> : IRepository<TEntity>
    where TEntity : IEntity
  {
    /// <summary>
    /// Find entity by Id.
    /// If entity with specified Id doesn't exist at storage then exception will be thrown.
    /// </summary>
    /// <param name="id">Id of entity.</param>
    /// <returns>Entity from the storage with passed Id.</returns>
    TEntity GetById(int id);
  }
}
