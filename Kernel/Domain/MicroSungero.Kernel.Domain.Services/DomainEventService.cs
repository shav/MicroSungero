using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MicroSungero.Kernel.Domain.Services
{
  /// <summary>
  /// An implementation of the service that notifies about domain events.
  /// </summary>
  public class DomainEventService : IDomainEventService
  {
    /// <summary>
    /// Publisher of domain events to the event bus.
    /// </summary>
    private readonly IPublisher eventPublisher;

    /// <summary>
    /// Create an instance of domain service.
    /// </summary>
    /// <param name="eventPublisher">Publisher of domain events to the event bus.</param>
    public DomainEventService(IPublisher eventPublisher)
    {
      this.eventPublisher = eventPublisher;
    }

    #region IDomainEventService

    public async Task Publish(IDomainEvent domainEvent)
    {
      await eventPublisher.Publish(domainEvent);
      if (domainEvent is IErrorHandlingDomainEvent errorHandlingEvent && errorHandlingEvent.Errors.Any())
      {
        throw errorHandlingEvent.Errors.Count >= 2 ? new AggregateException(errorHandlingEvent.Errors) : errorHandlingEvent.Errors.Single();
      }
    }

    #endregion
  }
}
