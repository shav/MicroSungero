using System;
using System.Collections.Concurrent;
using System.Threading;
using MicroSungero.Kernel.Domain.DomainEvents;
using MicroSungero.Kernel.Domain.Exceptions;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Transactional entities domain events scope.
  /// </summary>
  public class EntityDomainEventTransactionScope : IEntityDomainEventTransactionScope, IDisposable
  {
    #region IEntityDomainEventTransactionScope

    public void AddEvent(IEntityDomainEvent domainEvent)
    {
      this.CheckIfNotDisposed(nameof(AddEvent));

      var entityDomainEvents = this.domainEvents.GetOrAdd(new EntityIdentifier(domainEvent.Entity), e => new ConcurrentBag<IEntityDomainEvent>());
      entityDomainEvents.Add(domainEvent);
    }

    public void RaiseEvents(IEntity entity)
    {
      this.RaiseEvents(new EntityIdentifier(entity));
    }

    public void RaiseEvents(IEntityIdentifier entityIdentifier)
    {
      this.CheckIfNotDisposed(nameof(RaiseEvents));

      if (this.domainEvents.TryRemove(entityIdentifier, out var entityDomainEvents))
      {
        foreach (var domainEvent in entityDomainEvents)
          this.domainEventService.Publish(domainEvent);
      }
    }

    #endregion

    #region Properties and fields

    /// <summary>
    /// Current transactional domain events scope.
    /// </summary>
    public static EntityDomainEventTransactionScope Current => current.Value;

    private static AsyncLocal<EntityDomainEventTransactionScope> current = new AsyncLocal<EntityDomainEventTransactionScope>();

    /// <summary>
    /// Domain events cache.
    /// </summary>
    private ConcurrentDictionary<IEntityIdentifier, ConcurrentBag<IEntityDomainEvent>> domainEvents =
      new ConcurrentDictionary<IEntityIdentifier, ConcurrentBag<IEntityDomainEvent>>();

    /// <summary>
    /// Service that notifies subscribers when domain events raised.
    /// </summary>
    private IDomainEventService domainEventService;

    #endregion

    #region Methods

    /// <summary>
    /// Check before executing action if the current scope has not been disposed before.
    /// If this scope is disposed then throws exception.
    /// </summary>
    /// <param name="actionName">[Optional] Action name.</param>
    private void CheckIfNotDisposed(string actionName = default)
    {
      if (this.disposed)
        throw new InvalidOperationException($"Cannot perform action {actionName} because the current {nameof(EntityDomainEventTransactionScope)} is disposed.");
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create transactional domain events scope.
    /// </summary>
    /// <param name="domainEventService">Service that notifies subscribers when domain events raised.</param>
    public EntityDomainEventTransactionScope(IDomainEventService domainEventService)
    {
      // TODO: Allow wrapping EntityDomainEventTransactionScope
      if (EntityDomainEventTransactionScope.Current != null)
        throw new DomainException($"Cannot create new {nameof(EntityDomainEventTransactionScope)}: outer {nameof(EntityDomainEventTransactionScope)} already exists and wrapping {nameof(EntityDomainEventTransactionScope)}s is not allowed");

      this.domainEventService = domainEventService;
      EntityDomainEventTransactionScope.current.Value = this;
    }

    #endregion

    #region IDisposable

    private bool disposed;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          if (ReferenceEquals(EntityDomainEventTransactionScope.Current, this))
            EntityDomainEventTransactionScope.current.Value = null;
        }
        this.disposed = true;
      }
    }

    ~EntityDomainEventTransactionScope()
    {
      this.Dispose(false);
    }

    #endregion
  }
}
