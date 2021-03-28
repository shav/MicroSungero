using MicroSungero.Kernel.Domain.DomainEvents;
using System.Threading.Tasks;
using System.Threading;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base handler for any entity domain event.
  /// </summary>
  /// <typeparam name="TEntity">Type of entity.</typeparam>
  /// <typeparam name="TEvent">Type of domain event.</typeparam>
  public abstract class BaseEntityDomainEventHandler<TEntity, TEvent> : IDomainEventHandler<TEvent>
    where TEvent : EntityDomainEvent<TEntity>
    where TEntity : IEntity
  {
    #region IDomainEventHandler

    /// <summary>
    /// Handle the domain event.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public abstract Task Handle(TEvent domainEvent, CancellationToken cancellationToken);

    #endregion

    /// <summary>
    /// Entity validator.
    /// </summary>
    public IEntityValidationService<TEntity> validator;

    /// <summary>
    /// Create handler for any entity domain event.
    /// </summary>
    /// <param name="validator">Entity validator.</param>
    public BaseEntityDomainEventHandler(IEntityValidationService<TEntity> validator)
    {
      this.validator = validator;
    }
  }
}
