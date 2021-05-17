using System.Globalization;

namespace MicroSungero.Common.Logging
{
  /// <summary>
  /// Application logger configuration.
  /// </summary>
  public interface ILogConfiguration
  {
    #region Properties

    /// <summary>
    /// Application name
    /// </summary>
    string AppName { get; }

    /// <summary>
    /// Logging culture.
    /// </summary>
    CultureInfo LogCulture { get; }

    /// <summary>
    /// Path to directory with log files.
    /// </summary>
    string LogsPath { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Configure application logger.
    /// </summary>
    void Configure();

    /// <summary>
    /// Configure application logger.
    /// </summary>
    /// <param name="configFile">Config file name.</param>
    void Configure(string configFile);

    #endregion
  }
}
