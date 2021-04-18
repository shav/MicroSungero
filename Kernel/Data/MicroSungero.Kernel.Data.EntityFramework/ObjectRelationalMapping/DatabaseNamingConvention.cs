namespace MicroSungero.Kernel.Data.EntityFramework
{
  /// <summary>
  /// Database objects naming convention.
  /// </summary>
  public class DatabaseNamingConvention
  {
    #region Properties and fields

    /// <summary>
    /// Database server type.
    /// </summary>
    private DatabaseServerType databaseServerType;

    #endregion

    #region Methods

    /// <summary>
    /// Apply convention to table name.
    /// </summary>
    /// <param name="tableName">Original table name.</param>
    /// <returns></returns>
    public string ApplyToTableName(string tableName)
    {
      if (this.databaseServerType == DatabaseServerType.PostgreSQL)
      {
        return tableName?.ToLower();
      }
      return tableName;
    }

    /// <summary>
    /// Apply convention to column name.
    /// </summary>
    /// <param name="columnName">Original column name.</param>
    /// <returns></returns>
    public string ApplyToColumnName(string columnName)
    {
      if (this.databaseServerType == DatabaseServerType.PostgreSQL)
      {
        return columnName?.ToLower();
      }
      return columnName;
    }

    /// <summary>
    /// Apply convention to sequence name.
    /// </summary>
    /// <param name="sequenceName">Original sequence name.</param>
    /// <returns></returns>
    public string ApplyToSequenceName(string sequenceName)
    {
      if (this.databaseServerType == DatabaseServerType.PostgreSQL)
      {
        return sequenceName?.ToLower();
      }
      return sequenceName;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create database naming convention.
    /// </summary>
    /// <param name="databaseServerType">Database server type.</param>
    public DatabaseNamingConvention(DatabaseServerType databaseServerType)
    {
      this.databaseServerType = databaseServerType;
    }

    #endregion
  }
}
