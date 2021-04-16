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

    #endregion

    #region Constructors

    /// <summary>
    /// Create application settings.
    /// </summary>
    /// <param name="databaseSettings">Database settings.</param>
    public AppSettings(IDatabaseSettings databaseSettings)
    {
      this.DatabaseSettings = databaseSettings;
    }

    /// <summary>
    /// Create empty application settings.
    /// </summary>
    public AppSettings()
    {
      this.DatabaseSettings = new DatabaseSettings();
    }

    #endregion
  }
}
