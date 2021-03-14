using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MicroSungero.Kernel.Domain
{
  /// <summary>
  /// A handler for the certain type of domain events.
  /// Each type of domain events can have many handlers defined at different areas of the application.
  /// </summary>
  /// <typeparam name="TEvent">Domain event type.</typeparam>
  public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
  {
    /// <summary>
    /// Handle the domain event/
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task handling the domain event.</returns>
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
  }
}
