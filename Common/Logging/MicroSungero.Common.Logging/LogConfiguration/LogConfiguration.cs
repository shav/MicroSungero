using System;
using System.Globalization;
using System.IO;
using NLog;
using NLog.Config;

namespace MicroSungero.Common.Logging.NLog
{
  /// <summary>
  /// Application logger configuration.
  /// </summary>
  public sealed class LogConfiguration : ILogConfiguration
  {
    #region Constants

    /// <summary>
    /// Name of configuration variable for <see cref="LogsPath" />.
    /// </summary>
    private const string LOGS_PATH_VARIABLE = "logs-path";

    /// <summary>
    /// Name of configuration variable for <see cref="AppName" />.
    /// </summary>
    private const string APP_NAME_VARIABLE = "appname";

    #endregion

    #region Properties and fields

    /// <summary>
    /// Indicates that application start event has already been logged.
    /// </summary>
    private static bool isStartLogged;

    /// <summary>
    /// NLog configuration.
    /// </summary>
    private LoggingConfiguration configuration;

    /// <summary>
    /// Configuration loader.
    /// </summary>
    ILogConfigurationLoader configurationLoader;

    /// <summary>
    /// Path to directory with log files.
    /// </summary>
    private string logsPath;

    /// <summary>
    /// Application name.
    /// </summary>
    private string appName;

    #endregion

    #region Methods

    /// <summary>
    /// Log application started event.
    /// </summary>
    private static void LogApplicationStarted()
    {
      if (!isStartLogged)
      {
        var log = LogManager.GetLogger("Application");
        log.Trace(string.Empty);
        log.Trace("*********************** Application started ***********************");
        LogManager.Flush();
        isStartLogged = true;
      }
    }

    /// <summary>
    /// Set variable value at logger configuration.
    /// </summary>
    /// <param name="name">Variable name.</param>
    /// <param name="value">Variable value.</param>
    private void SetVariableValue(string name, string value)
    {
      if (this.configuration != null)
      {
        this.configuration.Variables[name] = value;
        LogManager.ReconfigExistingLoggers();
      }
    }

    /// <summary>
    /// Initialize logger configuration.
    /// </summary>
    private void InitConfiguration()
    {
      if (!string.IsNullOrWhiteSpace(this.LogsPath))
      {
        this.SetVariableValue(LOGS_PATH_VARIABLE, this.LogsPath);
      }
      if (!string.IsNullOrWhiteSpace(this.AppName))
      {
        this.SetVariableValue(APP_NAME_VARIABLE, this.AppName);
      }
    }

    #endregion

    #region ILoggerConfiguration

    public string AppName
    {
      get => this.appName;
      set
      {
        this.appName = value;
        this.SetVariableValue(APP_NAME_VARIABLE, value);
      }
    }

    public CultureInfo LogCulture { get; set; }


    public string LogsPath 
    {
      get => this.logsPath;
      set
      {
        this.logsPath = value;
        this.SetVariableValue(LOGS_PATH_VARIABLE, value);
      }
    }

    public void Configure()
    {
      this.Configure(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Logs.DefaultConfigFileName));
    }

    public void Configure(string configFile)
    {
      Exception loadConfigurationException = null;
      if (LogManager.Configuration == null)
      {
        try
        {
          this.configuration = this.configurationLoader.Load(configFile);
          this.InitConfiguration();
        }
        catch (Exception ex)
        {
          loadConfigurationException = ex;
        }
      }

      LogApplicationStarted();
      if (loadConfigurationException != null)
      {
        var log = LogManager.GetLogger(typeof(LogConfiguration).FullName);
        log.Warn(loadConfigurationException, "An error occurred while loading the logging configuration.");
      }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create log configuration.
    /// </summary>
    /// <param name="configurationLoader">Configuration loader.</param>
    public LogConfiguration(ILogConfigurationLoader configurationLoader)
    {
      this.configurationLoader = configurationLoader;
    }

    #endregion
  }
}
