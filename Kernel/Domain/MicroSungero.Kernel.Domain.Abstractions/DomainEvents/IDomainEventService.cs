using System.Threading.Tasks;

namespace MicroSungero.Kernel.Domain.DomainEvents
{
  /// <summary>
  /// A service notifying subscribers about domain events.
  /// </summary>
  public interface IDomainEventService
  {
    /// <summary>
    /// Notify subscribers that the domain event has just happened.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    /// <returns></returns>
    Task Publish(IDomainEvent domainEvent);
  }
}
