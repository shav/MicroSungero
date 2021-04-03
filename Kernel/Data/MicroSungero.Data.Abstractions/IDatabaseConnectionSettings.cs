using System.Data;

namespace MicroSungero.Data
{
  /// <summary>
  /// Database connection settings.
  /// </summary>
  public interface IDatabaseConnectionSettings
  {
    /// <summary>
    /// String with database connection parameters.
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// Transaction isolation level.
    /// </summary>
    IsolationLevel? TransactionIsolationLevel { get; }
  }
}
