using System;
using MicroSungero.Kernel.Domain.DomainEvents;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Service that manages entity lifetime.
  /// </summary>
  public class EntityLifetimeService : IEntityLifetimeService
  {
    #region IEntityLifetimeService

    public void OnBeforeDeleteEntity(IEntity entity)
    {
      this.PublishDomainEvent(typeof(EntityBeforeDeleteEvent<>), entity);
    }

    public void OnBeforeSaveEntity(IEntity entity)
    {
      this.PublishDomainEvent(typeof(EntityBeforeSaveEvent<>), entity);
    }

    public void OnEntityCreated(IEntity entity)
    {
      this.PublishDomainEvent(typeof(EntityCreatedEvent<>), entity);
    }

    public void OnEntityDeleted(IEntity entity)
    {
      this.PublishDomainEvent(typeof(EntityDeletedEvent<>), entity);
    }

    public void OnEntitySaved(IEntity entity)
    {
      this.PublishDomainEvent(typeof(EntitySavedEvent<>), entity);
    }

    #endregion

    #region Properties and fields

    /// <summary>
    /// Service notifying subscribers about domain events.
    /// </summary>
    private IDomainEventService domainEventService;

    #endregion

    #region Methods

    /// <summary>
    /// Publish entity lifetime domain event.
    /// </summary>
    /// <param name="eventType">Generic type of lifetime event.</param>
    /// <param name="entity">Entity.</param>
    private void PublishDomainEvent(Type eventType, IEntity entity)
    {
      eventType = eventType.MakeGenericType(entity.GetEntityInterface() ?? entity.GetType());
      this.domainEventService.Publish((IDomainEvent)Activator.CreateInstance(eventType, entity)).Wait();
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create entity lifetime service.
    /// </summary>
    /// <param name="domainEventService">Service notifying subscribers about domain events.</param>
    public EntityLifetimeService(IDomainEventService domainEventService)
    {
      this.domainEventService = domainEventService;
    }

    #endregion
  }
}
