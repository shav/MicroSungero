using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MicroSungero.Kernel.Domain.DomainEvents
{
  /// <summary>
  /// Domain event is an immutable model for something that happened within the domain bounded context.
  /// </summary>
  public abstract class DomainEvent : IDomainEvent, IErrorHandlingDomainEvent
  {
    #region IDomainEvent

    /// <summary>
    /// Domain event identifier.
    /// </summary>
    public Guid EventId { get; protected set; }

    /// <summary>
    /// Domain event type name.
    /// </summary>
    public string EventType { get; protected set; }

    /// <summary>
    /// Timestamp of the moment when the event has occured.
    /// </summary>
    public long Timestamp { get; protected set; }

    #endregion

    #region IErrorHandlingDomainEvent

    /// <summary>
    /// Errors which occured while handling domain event.
    /// </summary>
    public ICollection<Exception> Errors { get; } = new Collection<Exception>();

    #endregion

    #region Constructors

    public DomainEvent()
    {
      this.EventId = Guid.NewGuid();
      this.EventType = this.GetType().Name;
      this.Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    #endregion
  }
}
