using NLog.Config;

namespace MicroSungero.Common.Logging.NLog
{
  /// <summary>
  /// NLog configuration loader.
  /// </summary>
  public interface ILogConfigurationLoader
  {
    /// <summary>
    /// Load NLog configuration from file.
    /// </summary>
    /// <param name="configFile">Config file name.</param>
    /// <returns>NLog configuration.</returns>
    LoggingConfiguration Load(string configFile);
  }
}
