namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Event that is raised after new entity was created.
  /// </summary>
  /// <typeparam name="TEntity">Type of created entity.</typeparam>
  public class EntityCreatedEvent<TEntity> : EntityDomainEvent<TEntity>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create event after new entity was created.
    /// </summary>
    /// <param name="entity">Created entity.</param>
    public EntityCreatedEvent(TEntity entity)
      : base(entity)
    {
    }
  }
}
