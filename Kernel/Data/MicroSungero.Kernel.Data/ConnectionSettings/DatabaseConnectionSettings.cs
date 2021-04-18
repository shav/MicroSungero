using System.Data;

namespace MicroSungero.Kernel.Data
{
  /// <summary>
  /// Database connection settings.
  /// </summary>
  public class DatabaseConnectionSettings: IDatabaseConnectionSettings
  {
    #region IDatabaseConnectionSettings

    /// <summary>
    /// String with database connection parameters.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Transaction isolation level.
    /// </summary>
    public IsolationLevel? TransactionIsolationLevel { get; set; }

    /// <summary>
    /// Database server type.
    /// </summary>
    public DatabaseServerType ServerType { get; set; }

    #endregion
  }
}
