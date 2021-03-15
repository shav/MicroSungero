using MicroSungero.Kernel.Domain.DomainEvents;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base domain event generated from or for entities.
  /// </summary>
  /// <typeparam name="TEntity">Entity type.</typeparam>
  public abstract class EntityDomainEvent<TEntity> : DomainEvent, IEntityDomainEvent<TEntity>
  {
    #region IEntityDomainEvent

    public TEntity Entity { get; protected set; }

    #endregion

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
  public abstract class EntityDomainEvent : EntityDomainEvent<IEntity>, IEntityDomainEvent
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
