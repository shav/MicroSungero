using System;
using System.Collections.Generic;

namespace MicroSungero.Common.Logging
{
  /// <summary>
  /// Log message (event).
  /// </summary>
  public class LogEvent
  {
    /// <summary>
    /// Log message level.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// Message template with parameters.
    /// </summary>
    public string MessageTemplate { get; set; }

    /// <summary>
    /// Message parameters values.
    /// </summary>
    public object[] MessageArgs { get; set; }

    /// <summary>
    /// Exception for logging.
    /// </summary>
    public Exception Exception { get; set; }

    /// <summary>
    /// Custom properties of message.
    /// </summary>
    public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
  }
}
