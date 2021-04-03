using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MicroSungero.Kernel.Domain.DomainEvents;
using MicroSungero.Kernel.Domain.Entities;
using MicroSungero.Kernel.Domain.Exceptions;

namespace MicroSungero.Kernel.Domain.Services
{
  /// <summary>
  /// An implementation of the service that notifies about domain events.
  /// </summary>
  public class DomainEventService : IDomainEventService
  {
    #region Properties and fields

    /// <summary>
    /// Publisher of domain events to the event bus.
    /// </summary>
    private readonly IPublisher eventPublisher;

    /// <summary>
    /// Transactional domain events scope.
    /// </summary>
    private readonly IEntityDomainEventContext domainEventScope;

    #endregion

    #region Constructors

    /// <summary>
    /// Create an instance of domain service.
    /// </summary>
    /// <param name="eventPublisher">Publisher of domain events to the event bus.</param>
    /// <param name="domainEventScope">Transactional domain events scope.</param>
    public DomainEventService(IPublisher eventPublisher, IEntityDomainEventContext domainEventScope)
    {
      this.eventPublisher = eventPublisher;
      this.domainEventScope = domainEventScope;
    }

    #endregion

    #region IDomainEventService

    public async Task Publish(IDomainEvent domainEvent)
    {
      await eventPublisher.Publish(domainEvent);
      if (domainEvent is IErrorHandlingDomainEvent errorHandlingEvent && errorHandlingEvent.Errors.Any())
      {
        throw errorHandlingEvent.Errors.Count >= 2 ? new AggregateException(errorHandlingEvent.Errors) : errorHandlingEvent.Errors.Single();
      }
    }

    public async Task Publish(IEntityDomainEvent domainEvent, bool onTransactionCommit = false)
    {
      if (onTransactionCommit)
      {
        var domainEventsScope = this.domainEventScope.Current;
        if (domainEventsScope == null)
          throw new DomainException($"Cannot publish domain event on transaction commit: current {nameof(IEntityDomainEventTransactionScope)} does not exist.");

        domainEventsScope.AddEvent(domainEvent);
      }
      else
      {
        await this.Publish(domainEvent);
      }
    }

    #endregion
  }
}
