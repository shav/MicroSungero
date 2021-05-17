using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using NL = NLog;

namespace MicroSungero.Common.Logging.NLog
{
  /// <summary>
  /// Custom NLog logger.
  /// </summary>
  public class NLogWrapper : ILog
  {
    #region Constants

    /// <summary>
    /// Default log culture if it's not specified at config.
    /// </summary>
    private static readonly CultureInfo DEFAULT_LOG_CULTURE = CultureInfo.CreateSpecificCulture("en");

    #endregion

    #region Properties and fields

    /// <summary>
    /// Mapping our log message levels to NLog message levels.
    /// </summary>
    private static readonly IReadOnlyDictionary<LogLevel, NL.LogLevel> LogLevels = new Dictionary<LogLevel, NL.LogLevel>
    {
      [LogLevel.Trace] = NL.LogLevel.Trace,
      [LogLevel.Debug] = NL.LogLevel.Debug,
      [LogLevel.Info] = NL.LogLevel.Info,
      [LogLevel.Warn] = NL.LogLevel.Warn,
      [LogLevel.Error] = NL.LogLevel.Error,
      [LogLevel.Fatal] = NL.LogLevel.Fatal,
    };

    /// <summary>
    /// NLog logger.
    /// </summary>
    private readonly Lazy<NL.ILogger> logger;

    /// <summary>
    /// Custom properties for log messages.
    /// </summary>
    private readonly IDictionary<string, object> customProperties;

    /// <summary>
    /// Logger configuration.
    /// </summary>
    private readonly ILogConfiguration config;

    #endregion

    #region Methods

    /// <summary>
    /// Add custom properties to log message.
    /// </summary>
    /// <param name="logMessage">Log message.</param>
    /// <param name="customProperties">Custom properties</param>
    private static void AddCustomProperties(NL.LogEventInfo logMessage, IEnumerable<KeyValuePair<string, object>> customProperties)
    {
      foreach (var property in customProperties)
      {
        logMessage.Properties[property.Key] = property.Value;
      }
    }

    /// <summary>
    /// Execute action switching current thread to log culture.
    /// </summary>
    /// <param name="action">Action.</param>
    private void ExecuteWithLogCulture(Action action)
    {
      var culture = Thread.CurrentThread.CurrentCulture;
      var userInterfaceCulture = Thread.CurrentThread.CurrentUICulture;
      var defaultCulture = CultureInfo.DefaultThreadCurrentCulture;
      var defaultUserInterfaceCulture = CultureInfo.DefaultThreadCurrentUICulture;
      try
      {
        var logCulture = this.config?.LogCulture ?? DEFAULT_LOG_CULTURE;
        Thread.CurrentThread.CurrentCulture = logCulture;
        Thread.CurrentThread.CurrentUICulture = logCulture;
        CultureInfo.DefaultThreadCurrentCulture = logCulture;
        CultureInfo.DefaultThreadCurrentUICulture = logCulture;

        action();
      }
      finally
      {
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = userInterfaceCulture;
        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = defaultUserInterfaceCulture;
      }
    }

    #endregion

    #region ILog

    public bool IsDebugEnabled
    {
      get { return this.logger.Value.IsDebugEnabled; }
    }

    public bool IsTraceEnabled
    {
      get { return this.logger.Value.IsTraceEnabled; }
    }

    public bool IsWarnEnabled
    {
      get { return this.logger.Value.IsWarnEnabled; }
    }

    public bool IsErrorEnabled
    {
      get { return this.logger.Value.IsErrorEnabled; }
    }

    public bool IsInfoEnabled
    {
      get { return this.logger.Value.IsInfoEnabled; }
    }

    public bool IsFatalEnabled
    {
      get { return this.logger.Value.IsFatalEnabled; }
    }

    public void Info(string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Info, null, messageTemplate, args);
    }

    public void Info(Exception exception)
    {
      this.Log(LogLevel.Info, exception);
    }

    public void Info(Exception exception, string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Info, exception, messageTemplate, args);
    }

    public void Debug(string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Debug, null, messageTemplate, args);
    }

    public void Debug(Exception exception)
    {
      this.Log(LogLevel.Debug, exception);
    }

    public void Debug(Exception exception, string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Debug, exception, messageTemplate, args);
    }

    public void Trace(string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Trace, null, messageTemplate, args);
    }

    public void Trace(Exception exception)
    {
      this.Log(LogLevel.Trace, exception);
    }

    public void Trace(Exception exception, string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Trace, exception, messageTemplate, args);
    }

    public void Warn(string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Warn, null, messageTemplate, args);
    }

    public void Warn(Exception exception)
    {
      this.Log(LogLevel.Warn, exception);
    }

    public void Warn(Exception exception, string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Warn, exception, messageTemplate, args);
    }

    public void Error(string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Error, null, messageTemplate, args);
    }

    public void Error(Exception exception)
    {
      this.Log(LogLevel.Error, exception);
    }

    public void Error(Exception exception, string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Error, exception, messageTemplate, args);
    }

    public void Fatal(string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Fatal, null, messageTemplate, args);
    }

    public void Fatal(Exception exception)
    {
      this.Log(LogLevel.Fatal, exception);
    }

    public void Fatal(Exception exception, string messageTemplate, params object[] args)
    {
      this.Log(LogLevel.Fatal, exception, messageTemplate, args);
    }

    public void Log(LogLevel level, string messageTemplate, params object[] args)
    {
      this.Log(new LogEvent { LogLevel = level, MessageTemplate = messageTemplate, MessageArgs = args });
    }

    public void Log(LogLevel level, Exception exception)
    {
      this.Log(level, exception, null);
    }

    public void Log(LogLevel level, Exception exception, string messageTemplate, params object[] args)
    {
      this.Log(new LogEvent 
      { 
        LogLevel = level,
        MessageTemplate = messageTemplate,
        MessageArgs = args,
        Exception = exception
      });
    }

    public void Log(LogEvent logEvent)
    {
      this.ExecuteWithLogCulture(() =>
      {
        var messageTemplate = !string.IsNullOrEmpty(logEvent.MessageTemplate) ? logEvent.MessageTemplate : logEvent.Exception?.Message;
        
        var logMessage = new NL.LogEventInfo(LogLevels[logEvent.LogLevel], this.logger.Value.Name, null, messageTemplate, logEvent.MessageArgs, logEvent.Exception);
        AddCustomProperties(logMessage, logEvent.Properties.Union(this.customProperties));

        this.logger.Value.Log(logMessage);
      });
    }

    public ILog WithProperty(string propertyName, object propertyValue)
    {
      var newLogger = new NLogWrapper(this.logger.Value.Name, this.customProperties, this.config);
      newLogger.customProperties.Add(propertyName, propertyValue);
      return newLogger;
    }

    public ILog WithObject(object obj)
    {
      var newLogger = new NLogWrapper(this.logger.Value.Name, this.customProperties, this.config);
      var type = obj.GetType();
      foreach (var property in type.GetProperties())
      {
        newLogger.customProperties.Add(property.Name, property.GetValue(obj));
      }
      return newLogger;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create logger.
    /// </summary>
    /// <param name="loggerName">Logger name.</param>
    /// <param name="config">Logger configuration.</param>
    public NLogWrapper(string loggerName, ILogConfiguration config)
      : this(loggerName, new Dictionary<string, object>(), config)
    {
    }

    /// <summary>
    /// Create logger.
    /// </summary>
    /// <param name="loggerName">Logger name.</param>
    /// <param name="customProperties">Custom log message properties.</param>
    /// <param name="config">Logger configuration.</param>
    public NLogWrapper(string loggerName, IDictionary<string, object> customProperties, ILogConfiguration config)
    {
      this.logger = new Lazy<NL.ILogger>(() => NL.LogManager.GetLogger(loggerName));
      this.customProperties = new Dictionary<string, object>(customProperties);
      this.config = config;
    }

    #endregion
  }
}
