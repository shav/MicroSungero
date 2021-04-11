using System;
using System.Threading;
using System.Threading.Tasks;
using MicroSungero.Kernel.Domain.DomainEvents;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base handler of event raised after new entity was created.
  /// </summary>
  /// <typeparam name="TEntity">Type of created entity.</typeparam>
  public abstract class EntityCreatedEventHandler<TEntity>
    : BaseEntityDomainEventHandler<TEntity, EntityCreatedEvent<TEntity>>, IDomainEventHandler<EntityCreatedEvent<TEntity>>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create handler of event raised after entity was created.
    /// </summary>
    /// <param name="validator">Entity validator.</param>
    public EntityCreatedEventHandler(IEntityValidationService<TEntity> validator)
      : base(validator)
    {
    }

    #region IDomainEventHandler

    public override async Task Handle(EntityCreatedEvent<TEntity> domainEvent, CancellationToken cancellationToken)
    {
      await this.HandleEventCore(domainEvent, cancellationToken);

      try
      {
        this.validator.Validate<ICreateEntityValidator<TEntity>>(domainEvent.Entity);
      }
      catch (Exception validationError)
      {
        domainEvent.Errors.Add(validationError);
      }
    }

    #endregion

    /// <summary>
    /// Core logic of handling event raised after entity was created.
    /// </summary>
    /// <param name="domainEvent">Event raised after entity was created.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    protected abstract Task HandleEventCore(EntityCreatedEvent<TEntity> domainEvent, CancellationToken cancellationToken);
  }
}
