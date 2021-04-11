using MicroSungero.Kernel.Domain.DomainEvents;
using System.Threading.Tasks;
using System.Threading;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base handler of event raised after entity was saved.
  /// </summary>
  /// <typeparam name="TEntity">Type of saved entity.</typeparam>
  public abstract class EntitySavedEventHandler<TEntity>
    : BaseEntityDomainEventHandler<TEntity, EntitySavedEvent<TEntity>>, IDomainEventHandler<EntitySavedEvent<TEntity>>
    where TEntity : class, IEntity
  {
    /// <summary>
    /// Create handler of event raised after entity was saved.
    /// </summary>
    /// <param name="validator">Entity validator.</param>
    public EntitySavedEventHandler(IEntityValidationService<TEntity> validator)
      : base(validator)
    {
    }

    #region IDomainEventHandler

    public override async Task Handle(EntitySavedEvent<TEntity> domainEvent, CancellationToken cancellationToken)
    {
      await this.HandleEventCore(domainEvent, cancellationToken);
    }

    #endregion

    /// <summary>
    /// Core logic of handling event raised after entity was saved.
    /// </summary>
    /// <param name="domainEvent">Event raised after entity was saved.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    protected abstract Task HandleEventCore(EntitySavedEvent<TEntity> domainEvent, CancellationToken cancellationToken);
  }
}
