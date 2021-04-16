using System.Data;

namespace MicroSungero.WebAPI.Settings
{
  /// <summary>
  /// Database settings.
  /// </summary>
  public class DatabaseSettings
  {
    /// <summary>
    /// Connection string.
    /// </summary>
    public string ConnectionString { get; private set; }

    /// <summary>
    /// Database transactions isolation level.
    /// </summary>
    public IsolationLevel TransactionIsolationLevel { get; private set; }
  }
}
