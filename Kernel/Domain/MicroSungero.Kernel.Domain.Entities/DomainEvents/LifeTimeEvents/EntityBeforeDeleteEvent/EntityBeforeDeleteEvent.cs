namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Event that is raised before deleting entity.
  /// </summary>
  /// <typeparam name="TEntity">Type of deleting entity.</typeparam>
  public class EntityBeforeDeleteEvent<TEntity> : EntityDomainEvent<TEntity>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create event before deleting entity.
    /// </summary>
    /// <param name="entity">Deleting entity.</param>
    public EntityBeforeDeleteEvent(TEntity entity)
      : base(entity)
    {
    }
  }
}
