namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Event that is raised before saving entity.
  /// </summary>
  /// <typeparam name="TEntity">Type of saving entity.</typeparam>
  public class EntityBeforeSaveEvent<TEntity> : EntityDomainEvent<TEntity>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create event before saving entity.
    /// </summary>
    /// <param name="entity">Saving entity.</param>
    public EntityBeforeSaveEvent(TEntity entity)
      : base(entity)
    {
    }
  }
}
