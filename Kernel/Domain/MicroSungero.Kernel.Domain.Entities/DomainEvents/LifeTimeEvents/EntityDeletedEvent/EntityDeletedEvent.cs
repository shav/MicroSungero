namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Event that is raised after entity was deleted.
  /// </summary>
  /// <typeparam name="TEntity">Type of deleted entity.</typeparam>
  public class EntityDeletedEvent<TEntity> : EntityDomainEvent<TEntity>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create event after entity was deleted.
    /// </summary>
    /// <param name="entity">Deleted entity.</param>
    public EntityDeletedEvent(TEntity entity)
      : base(entity)
    {
    }
  }
}
