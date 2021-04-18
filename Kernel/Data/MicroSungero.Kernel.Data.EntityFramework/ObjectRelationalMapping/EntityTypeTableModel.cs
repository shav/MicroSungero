using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroSungero.Kernel.Data.EntityFramework
{
  /// <summary>
  /// Model for mapping entity type do database table.
  /// </summary>
  public class EntityTypeTableModel
  {
    #region Properties

    /// <summary>
    /// Table name.
    /// </summary>
    public string TableName { get; }

    /// <summary>
    /// Properties mapping.
    /// </summary>
    public ICollection<PropertyBuilder> Properties { get; } = new List<PropertyBuilder>();

    #endregion

    #region Constructors

    /// <summary>
    /// Create mapping model.
    /// </summary>
    /// <param name="tableName">Database table name.</param>
    public EntityTypeTableModel(string tableName)
    {
      this.TableName = tableName;
    }

    /// <summary>
    /// Create empty mapping model.
    /// </summary>
    public EntityTypeTableModel()
    {
    }

    #endregion
  }
}
