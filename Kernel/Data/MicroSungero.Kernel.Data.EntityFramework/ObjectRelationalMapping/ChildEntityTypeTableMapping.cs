using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Data.EntityFramework
{
  /// <summary>
  /// Base object-relational mapping for child entity type to database table.
  /// </summary>
  /// <typeparam name="TChildEntity">Type of child entity.</typeparam>
  public abstract class ChildEntityTypeTableMapping<TChildEntity> : EntityTypeTableMapping<TChildEntity>
    where TChildEntity : ChildEntity
  {
    #region EntityTypeToTableMapping

    protected override EntityTypeTableModel BuildEntityModel(EntityTypeBuilder<TChildEntity> builder)
    {
      var model = base.BuildEntityModel(builder);
      builder.Ignore(t => t.RootEntity);

      return model;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create object-relational mapping for child entities.
    /// </summary>
    /// <param name="connectionSettings">Database connection settings.</param>
    protected ChildEntityTypeTableMapping(IDatabaseConnectionSettings connectionSettings)
      : base(connectionSettings)
    {
    }

    #endregion

  }
}
