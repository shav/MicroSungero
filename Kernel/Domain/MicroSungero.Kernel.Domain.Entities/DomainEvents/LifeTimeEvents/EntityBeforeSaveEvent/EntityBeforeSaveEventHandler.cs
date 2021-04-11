using System;
using MicroSungero.Kernel.Domain.DomainEvents;
using System.Threading.Tasks;
using System.Threading;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base handler of event raised before saving entity.
  /// </summary>
  /// <typeparam name="TEntity">Type of saving entity.</typeparam>
  public abstract class EntityBeforeSaveEventHandler<TEntity>
    : BaseEntityDomainEventHandler<TEntity, EntityBeforeSaveEvent<TEntity>>, IDomainEventHandler<EntityBeforeSaveEvent<TEntity>>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create handler of event raised before saving entity.
    /// </summary>
    /// <param name="validator">Entity validator.</param>
    public EntityBeforeSaveEventHandler(IEntityValidationService<TEntity> validator)
      : base(validator)
    {
    }

    #region IDomainEventHandler

    public override async Task Handle(EntityBeforeSaveEvent<TEntity> domainEvent, CancellationToken cancellationToken)
    {
      try
      {
        this.validator.Validate<ISaveEntityValidator<TEntity>>(domainEvent.Entity);
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
    /// Core logic of handling event raised before saving entity.
    /// </summary>
    /// <param name="domainEvent">Event raised before saving entity.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    protected abstract Task HandleEventCore(EntityBeforeSaveEvent<TEntity> domainEvent, CancellationToken cancellationToken);
  }
}
