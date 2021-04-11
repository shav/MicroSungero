using MicroSungero.Kernel.Domain.DomainEvents;
using System.Threading.Tasks;
using System.Threading;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base handler of event raised after entity was deleted.
  /// </summary>
  /// <typeparam name="TEntity">Type of deleted entity.</typeparam>
  public abstract class EntityDeletedEventHandler<TEntity>
    : BaseEntityDomainEventHandler<TEntity, EntityDeletedEvent<TEntity>>, IDomainEventHandler<EntityDeletedEvent<TEntity>>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create handler of event raised after entity was deleted.
    /// </summary>
    /// <param name="validator">Entity validator.</param>
    public EntityDeletedEventHandler(IEntityValidationService<TEntity> validator)
      : base(validator)
    {
    }

    #region IDomainEventHandler

    public override async Task Handle(EntityDeletedEvent<TEntity> domainEvent, CancellationToken cancellationToken)
    {
      await this.HandleEventCore(domainEvent, cancellationToken);
    }

    #endregion

    /// <summary>
    /// Core logic of handling event raised after entity was deleted.
    /// </summary>
    /// <param name="domainEvent">Event raised after entity was deleted.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    protected abstract Task HandleEventCore(EntityDeletedEvent<TEntity> domainEvent, CancellationToken cancellationToken);
  }
}
