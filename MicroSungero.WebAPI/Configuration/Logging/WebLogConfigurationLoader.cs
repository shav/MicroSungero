using NLog.Config;
using NL = NLog;

namespace MicroSungero.Common.Logging.NLog
{
  /// <summary>
  /// NLog configuration loader for web applications.
  /// </summary>
  public class WebLogConfigurationLoader : ILogConfigurationLoader
  {
    #region ILogConfigurationLoader

    public LoggingConfiguration Load(string configFile)
    {
      return NL.Web.NLogBuilder.ConfigureNLog(configFile).Configuration;
    }

    #endregion
  }
}
