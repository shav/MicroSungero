using System;

namespace MicroSungero.Common.Logging
{
  /// <summary>
  /// Logger.
  /// </summary>
  public interface ILog
  {
    #region Properties

    /// <summary>
    /// Indicates that logging of Debug messages allowed.
    /// </summary>
    bool IsDebugEnabled { get; }

    /// <summary>
    /// Indicates that logging of Trace messages allowed.
    /// </summary>
    bool IsTraceEnabled { get; }

    /// <summary>
    /// Indicates that logging of Warn messages allowed.
    /// </summary>
    bool IsWarnEnabled { get; }

    /// <summary>
    /// Indicates that logging of Error messages allowed.
    /// </summary>
    bool IsErrorEnabled { get; }

    /// <summary>
    /// Indicates that logging of Info messages allowed.
    /// </summary>
    bool IsInfoEnabled { get; }

    /// <summary>
    /// Indicates that logging of Fatal messages allowed.
    /// </summary>
    bool IsFatalEnabled { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Write message of Info level to log.
    /// </summary>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Info(string messageTemplate, params object[] args);

    /// <summary>
    /// Write exception message of Info level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    void Info(Exception exception);

    /// <summary>
    /// Write exception message of Info level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Info(Exception exception, string messageTemplate, params object[] args);

    /// <summary>
    /// Write message of Debug level to log.
    /// </summary>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Debug(string messageTemplate, params object[] args);

    /// <summary>
    /// Write exception message of Debug level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    void Debug(Exception exception);

    /// <summary>
    /// Write exception message of Debug level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Debug(Exception exception, string messageTemplate, params object[] args);

    /// <summary>
    /// Write message of Trace level to log.
    /// </summary>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Trace(string messageTemplate, params object[] args);

    /// <summary>
    /// Write exception message of Trace level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    void Trace(Exception exception);

    /// <summary>
    /// Write exception message of Trace level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Trace(Exception exception, string messageTemplate, params object[] args);

    /// <summary>
    /// Write message of Warn level to log.
    /// </summary>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Warn(string messageTemplate, params object[] args);

    /// <summary>
    /// Write exception message of Warn level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    void Warn(Exception exception);

    /// <summary>
    /// Write exception message of Warn level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Warn(Exception exception, string messageTemplate, params object[] args);

    /// <summary>
    /// Write message of Error level to log.
    /// </summary>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Error(string messageTemplate, params object[] args);

    /// <summary>
    /// Write exception message of Error level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    void Error(Exception exception);

    /// <summary>
    /// Write exception message of Error level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Error(Exception exception, string messageTemplate, params object[] args);

    /// <summary>
    /// Write message of Fatal level to log.
    /// </summary>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Fatal(string messageTemplate, params object[] args);

    /// <summary>
    /// Write exception message of Fatal level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    void Fatal(Exception exception);

    /// <summary>
    /// Write exception message of Fatal level to log.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Fatal(Exception exception, string messageTemplate, params object[] args);

    /// <summary>
    /// Write message to log.
    /// </summary>
    /// <param name="level">Message level.</param>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Log(LogLevel level, string messageTemplate, params object[] args);

    /// <summary>
    /// Write exception message to log.
    /// </summary>
    /// <param name="level">Message level.</param>
    /// <param name="exception">Exception.</param>
    void Log(LogLevel level, Exception exception);

    /// <summary>
    /// Write exception message to log.
    /// </summary>
    /// <param name="level">Message level.</param>
    /// <param name="exception">Exception.</param>
    /// <param name="messageTemplate">Message template with parameters.</param>
    /// <param name="args">Message parameters values.</param>
    void Log(LogLevel level, Exception exception, string messageTemplate, params object[] args);

    /// <summary>
    /// Write message to log.
    /// </summary>
    /// <param name="logEvent">Message.</param>
    void Log(LogEvent logEvent);

    /// <summary>
    /// Add custom property to log message.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <param name="propertyValue">Property value.</param>
    ILog WithProperty(string propertyName, object propertyValue);

    /// <summary>
    /// Add custom value to log message.
    /// </summary>
    /// <param name="obj">Custom value.</param>
    ILog WithObject(object obj);

    #endregion
  }
}
