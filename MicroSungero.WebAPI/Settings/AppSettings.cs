namespace MicroSungero.WebAPI.Settings
{
  /// <summary>
  /// Application settings.
  /// </summary>
  public class AppSettings
  {
    #region Properties

    /// <summary>
    /// Database settings.
    /// </summary>
    public IDatabaseSettings DatabaseSettings { get; }

    /// <summary>
    /// Log settings.
    /// </summary>
    public ILogSettings LogSettings { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Create application settings.
    /// </summary>
    /// <param name="databaseSettings">Database settings.</param>
    /// <param name="logSettings">Log settings.</param>
    public AppSettings(IDatabaseSettings databaseSettings, ILogSettings logSettings)
    {
      this.DatabaseSettings = databaseSettings;
      this.LogSettings = logSettings;
    }

    /// <summary>
    /// Create empty application settings.
    /// </summary>
    public AppSettings()
    {
      this.DatabaseSettings = new DatabaseSettings();
      this.LogSettings = new LogSettings();
    }

    #endregion
  }
}
