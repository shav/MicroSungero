using System;
using System.Collections.Generic;
using MediatR;

namespace MicroSungero.Kernel.Domain
{
  /// <summary>
  /// An immutable model for something that happened within the domain bounded context.
  /// </summary>
  public interface IDomainEvent: INotification
  {
    /// <summary>
    /// Domain event identifier.
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// Domain event type name.
    /// </summary>
    string EventType { get; }

    /// <summary>
    /// Timestamp of the moment when the event has occured.
    /// </summary>
    long Timestamp { get; }
  }

  /// <summary>
  /// A domain event that aggregates errors occured while handling event.
  /// </summary>
  public interface IErrorHandlingDomainEvent: IDomainEvent
  {
    /// <summary>
    /// Errors which occured while handling domain event.
    /// </summary>
    ICollection<Exception> Errors { get; }
  }
}
