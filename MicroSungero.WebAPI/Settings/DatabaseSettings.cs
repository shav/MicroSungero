﻿using System.Data;

namespace MicroSungero.WebAPI.Settings
{
  /// <summary>
  /// Database settings (immutable).
  /// </summary>
  public interface IDatabaseSettings
  {
    /// <summary>
    /// Connection string.
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// Database transactions isolation level.
    /// </summary>
    IsolationLevel TransactionIsolationLevel { get; }
  }

  /// <summary>
  /// Database settings.
  /// </summary>
  public class DatabaseSettings: IDatabaseSettings
  {
    #region Constants

    /// <summary>
    /// Database setting name at config.
    /// </summary>
    public const string SettingName = "Database";

    #endregion

    #region IDatabaseSettings

    /// <summary>
    /// Connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Database transactions isolation level.
    /// </summary>
    public IsolationLevel TransactionIsolationLevel { get; set; }

    #endregion
  }
}
