using NLog;
using NLog.Config;

namespace MicroSungero.Common.Logging.NLog
{
  /// <summary>
  /// Default NLog configuration loader.
  /// </summary>
  public class DefaultLogConfigurationLoader : ILogConfigurationLoader
  {
    #region ILogConfigurationLoader

    public LoggingConfiguration Load(string configFile)
    {
      return LogManager.LoadConfiguration(configFile)?.Configuration;
    }

    #endregion
  }
}
