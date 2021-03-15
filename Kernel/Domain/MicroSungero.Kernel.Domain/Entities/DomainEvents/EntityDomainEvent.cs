using MicroSungero.Kernel.Domain.DomainEvents;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base domain event generated from or for entities.
  /// </summary>
  /// <typeparam name="TEntity">Entity type.</typeparam>
  public abstract class EntityDomainEvent<TEntity> : DomainEvent
  {
    /// <summary>
    /// Entity which domain event generated for.
    /// </summary>
    public TEntity Entity { get; protected set; }

    /// <summary>
    /// Create entity domain event.
    /// </summary>
    /// <param name="entity">Entity which domain event generated for.</param>
    protected EntityDomainEvent(TEntity entity)
    {
      this.Entity = entity;
    }
  }

  /// <summary>
  /// Base domain event generated from or for entities.
  /// </summary>
  public abstract class EntityDomainEvent : EntityDomainEvent<IEntity>
  {
    /// <summary>
    /// Create entity domain event.
    /// </summary>
    /// <param name="entity">Entity which domain event generated for.</param>
    protected EntityDomainEvent(IEntity entity)
      : base(entity)
    {
    }
  }
}
