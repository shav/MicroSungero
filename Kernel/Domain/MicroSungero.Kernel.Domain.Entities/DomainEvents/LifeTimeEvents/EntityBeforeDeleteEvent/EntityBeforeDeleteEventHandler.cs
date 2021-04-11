using System;
using System.Threading;
using System.Threading.Tasks;
using MicroSungero.Kernel.Domain.DomainEvents;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base handler of event raised before deleting entity.
  /// </summary>
  /// <typeparam name="TEntity">Type of deleting entity.</typeparam>
  public abstract class EntityBeforeDeleteEventHandler<TEntity>
    : BaseEntityDomainEventHandler<TEntity, EntityBeforeDeleteEvent<TEntity>>, IDomainEventHandler<EntityBeforeDeleteEvent<TEntity>>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create handler of event raised before deleting entity.
    /// </summary>
    /// <param name="validator">Entity validator.</param>
    public EntityBeforeDeleteEventHandler(IEntityValidationService<TEntity> validator)
      : base(validator)
    {
    }

    #region IDomainEventHandler

    public override async Task Handle(EntityBeforeDeleteEvent<TEntity> domainEvent, CancellationToken cancellationToken)
    {
      try
      {
        this.validator.Validate<IDeleteEntityValidator<TEntity>>(domainEvent.Entity);
      }
      catch (Exception validationError)
      {
        domainEvent.Errors.Add(validationError);
        return;
      }
      
      await this.HandleEventCore(domainEvent, cancellationToken);
    }

    #endregion

    /// <summary>
    /// Core logic of handling event raised before deleting entity.
    /// </summary>
    /// <param name="domainEvent">Event raised before deleting entity.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    protected abstract Task HandleEventCore(EntityBeforeDeleteEvent<TEntity> domainEvent, CancellationToken cancellationToken);
  }
}
