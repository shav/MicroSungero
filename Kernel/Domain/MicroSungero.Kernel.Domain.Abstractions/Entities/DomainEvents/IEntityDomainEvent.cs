using MicroSungero.Kernel.Domain.DomainEvents;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base domain event generated from or for entities.
  /// </summary>
  /// <typeparam name="TEntity">Type of entities.</typeparam>
  public interface IEntityDomainEvent<out TEntity> : IDomainEvent
  {
    /// <summary>
    /// Entity which domain event generated for.
    /// </summary>
    TEntity Entity { get; }
  }

  /// <summary>
  /// Base domain event generated from or for entities.
  /// </summary>
  public interface IEntityDomainEvent: IEntityDomainEvent<IEntity>
  {
  }
}
