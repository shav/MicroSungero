namespace MicroSungero.WebAPI.Settings
{
  /// <summary>
  /// Log settings (immutable).
  /// </summary>
  public interface ILogSettings
  {
    /// <summary>
    /// Logging culture.
    /// </summary>
    string LogCulture { get; }

    /// <summary>
    /// Path to directory with log files.
    /// </summary>
    string LogsPath { get; }
  }

  /// <summary>
  /// Log settings.
  /// </summary>
  public class LogSettings: ILogSettings
  {
    #region Constants

    /// <summary>
    /// Log setting name at config.
    /// </summary>
    public const string SettingName = "Logging";

    #endregion

    #region ILogSettings

    /// <summary>
    /// Logging culture.
    /// </summary>
    public string LogCulture { get; set; }

    /// <summary>
    /// Path to directory with log files.
    /// </summary>
    public string LogsPath { get; set; }

    #endregion
  }
}
