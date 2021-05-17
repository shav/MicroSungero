using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicroSungero.Common.Logging;
using MicroSungero.Common.Logging.NLog;

namespace MicroSungero.WebAPI.Configuration
{
  /// <summary>
  /// Extension methods for logging configuration.
  /// </summary>
  public static class LogConfigureExtensions
  {
    /// <summary>
    /// Configure application logger.
    /// </summary>
    /// <param name="services">Dependency container.</param>
    /// <param name="configuration">App configuration.</param>
    /// <param name="serviceName">Service name.</param>
    public static void UseLogger(this IServiceCollection services, IConfiguration configuration, string serviceName)
    {
      services.AddSingleton<ILogFactory, LogFactory>();
      services.AddSingleton<ILogManager, NLogManager>();
      services.AddSingleton<ILogConfiguration, LogConfiguration>(p =>
      {
        var config = new LogConfiguration(new WebLogConfigurationLoader());
        var logSettings = configuration.GetAppSettings()?.LogSettings;
        config.LogsPath = logSettings?.LogsPath;
        var logCulture = logSettings?.LogCulture;
        config.LogCulture = !string.IsNullOrWhiteSpace(logCulture) ? CultureInfo.CreateSpecificCulture(logCulture) : null;
        config.AppName = serviceName;
        return config;
      });

      var provider = services.BuildServiceProvider();
      var logFactory = provider.GetService<ILogFactory>();
      var logManager = provider.GetService<ILogManager>();
      var logConfig = provider.GetService<ILogConfiguration>();
      logConfig.Configure();
      Logs.Init(logFactory, logConfig, logManager);
    }
  }
}
