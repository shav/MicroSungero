namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Event that is raised after entity was saved.
  /// </summary>
  /// <typeparam name="TEntity">Type of saved entity.</typeparam>
  public class EntitySavedEvent<TEntity> : EntityDomainEvent<TEntity>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create event after entity was saved.
    /// </summary>
    /// <param name="entity">Saved entity.</param>
    public EntitySavedEvent(TEntity entity)
      : base(entity)
    {
    }
  }
}
