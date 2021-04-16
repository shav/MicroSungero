using Microsoft.Extensions.Configuration;
using MicroSungero.WebAPI.Settings;

namespace MicroSungero.WebAPI.Configuration
{
  /// <summary>
  /// Application settings configure extensions.
  /// </summary>
  public static class AppSettingsConfigureExtensions
  {
    /// <summary>
    /// Get application settings from configuration.
    /// </summary>
    /// <param name="configuration">App configuration.</param>
    /// <returns></returns>
    public static AppSettings GetAppSettings(this IConfiguration configuration)
    {
      var databaseSettings = configuration.GetSection(DatabaseSettings.SettingName).Get<DatabaseSettings>();
      return new AppSettings(databaseSettings);
    }
  }
}
