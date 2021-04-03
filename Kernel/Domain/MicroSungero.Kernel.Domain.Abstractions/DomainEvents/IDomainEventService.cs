using System.Threading.Tasks;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Domain.DomainEvents
{
  /// <summary>
  /// A service notifying subscribers about domain events.
  /// </summary>
  public interface IDomainEventService
  {
    /// <summary>
    /// Notify subscribers that the domain event just happened.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    Task Publish(IDomainEvent domainEvent);

    /// <summary>
    /// Notify subscribers that the entity domain event happened.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    /// <param name="onTransactionCommit">Indicates that event should be raised only after transaction successfull completed.</param>
    Task Publish(IEntityDomainEvent domainEvent, bool onTransactionCommit = false);
  }
}
