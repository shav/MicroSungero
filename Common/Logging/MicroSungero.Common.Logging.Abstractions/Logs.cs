using System;
using System.Collections.Concurrent;
using System.Linq;

namespace MicroSungero.Common.Logging
{
  /// <summary>
  /// Logging API.
  /// </summary>
  public static class Logs
  {
    #region Constants

    /// <summary>
    /// Log configuration file name.
    /// </summary>
    public const string DefaultConfigFileName = "LogSettings.config";

    #endregion

    #region Properties and fields

    private static ILogConfiguration configuration;

    /// <summary>
    /// Log configuration.
    /// </summary>
    public static ILogConfiguration Сonfiguration => configuration;

    /// <summary>
    /// Log factory.
    /// </summary>
    private static ILogFactory logFactory;

    /// <summary>
    /// Logs manager.
    /// </summary>
    private static ILogManager logManager;

    /// <summary>
    /// Loggers cache.
    /// </summary>
    private static readonly ConcurrentDictionary<string, ILog> loggers = new ConcurrentDictionary<string, ILog>();

    #endregion

    #region Methods

    /// <summary>
    /// Flush cached messages to log.
    /// </summary>
    public static void Flush()
    {
      logManager.Flush();
    }

    /// <summary>
    /// Get logger for type.
    /// </summary>
    /// <typeparam name="T">Type of logger.</typeparam>
    /// <returns>Logger.</returns>
    public static ILog GetLogger<T>()
    {
      return GetLogger(typeof(T));
    }

    /// <summary>
    /// Get logger for type.
    /// </summary>
    /// <param name="type">Type of logger.</param>
    /// <returns>Logger.</returns>
    public static ILog GetLogger(Type type)
    {
      if (!type.IsGenericType)
        return GetLogger(type.FullName);

      var genericTypeName = type.GetGenericTypeDefinition().FullName.Split('`').First();
      var genericTypeArguments = string.Join(", ", type.GenericTypeArguments.Select(t => t.Name));
      return GetLogger($"{genericTypeName}<{genericTypeArguments}>");
    }

    /// <summary>
    /// Get logger.
    /// </summary>
    /// <param name="loggerName">Logger name.</param>
    /// <returns>Logger.</returns>
    public static ILog GetLogger(string loggerName)
    {
      return loggers.GetOrAdd(loggerName, name => logFactory.CreateLogger(name));
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Initialize Logs API.
    /// </summary>
    /// <param name="logFactory">Log factory.</param>
    /// <param name="configuration">Log configuration.</param>
    /// <param name="logManager">Logs manager.</param>
    public static void Init(ILogFactory logFactory, ILogConfiguration configuration, ILogManager logManager)
    {
      if (Logs.logFactory != null)
        throw new InvalidOperationException($"Cannot initialize Logs API: {nameof(Logs.logFactory)} has already been initialized");
      if (Logs.configuration != null)
        throw new InvalidOperationException($"Cannot initialize Logs API: {nameof(Logs.configuration)} has already been initialized");
      if (Logs.logManager != null)
        throw new InvalidOperationException($"Cannot initialize Logs API: {nameof(Logs.logManager)} has already been initialized");

      if (logFactory == null)
        throw new ArgumentNullException(nameof(logFactory));
      if (configuration == null)
        throw new ArgumentNullException(nameof(configuration));
      if (logManager == null)
        throw new ArgumentNullException(nameof(logManager));

      Logs.logFactory = logFactory;
      Logs.configuration = configuration;
      Logs.logManager = logManager;
    }

    #endregion
  }
}
