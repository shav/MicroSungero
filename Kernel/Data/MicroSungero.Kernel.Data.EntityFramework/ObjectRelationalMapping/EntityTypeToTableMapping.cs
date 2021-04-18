using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicroSungero.Kernel.Domain;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Data.EntityFramework
{
  /// <summary>
  /// Base object-relational mapping for entity type to database table.
  /// </summary>
  /// <typeparam name="TEntity">Type of entity.</typeparam>
  public abstract class EntityTypeToTableMapping<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
  {
    #region Properties and fields

    /// <summary>
    /// Table name in database for entities.
    /// </summary>
    public string TableName
    {
      get
      { 
        var tableName = $"{this.ModuleName.Replace('.', '_')}_{typeof(TEntity).Name}";
        return this.connectionSettings.ServerType == DatabaseServerType.PostgreSQL ? tableName.ToLower() : tableName;
      }
    }

    /// <summary>
    /// Module name which entity type is defined at.
    /// </summary>
    protected abstract string ModuleName { get; }

    /// <summary>
    /// Database connection settings.
    /// </summary>
    private IDatabaseConnectionSettings connectionSettings;

    #endregion

    #region IEntityTypeConfiguration

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
      var model = this.BuildEntityModel(builder);

      if (connectionSettings.ServerType == DatabaseServerType.PostgreSQL)
      {
        foreach (var property in model.Properties)
        {
          var propertyName = property.Metadata.Name;
          if (propertyName != null)
            property.HasColumnName(propertyName.ToLower());
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Build model for mapping entity type do database table.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    /// <returns>Model for mapping entity type do database table.</returns>
    protected virtual EntityTypeTableModel BuildEntityModel(EntityTypeBuilder<TEntity> builder)
    {
      var model = new EntityTypeTableModel(this.TableName);

      builder.ToTable(this.TableName);

      builder.Ignore(t => ((IPersistentObject)t).IsTransient);
      builder.Ignore(t => ((IPersistentObject)t).IsDeleted);
      builder.Ignore(t => t.DisplayValue);

      builder.HasKey(t => t.Id);
      var Id = builder.Property(t => t.Id).IsRequired();
      var idSequenceName = $"{this.TableName}_Id";
      switch (this.connectionSettings.ServerType)
      {
        case DatabaseServerType.MSSQLServer:
          SqlServerPropertyBuilderExtensions.UseHiLo(Id, idSequenceName);
          break;
        case DatabaseServerType.PostgreSQL:
          NpgsqlPropertyBuilderExtensions.UseHiLo(Id, idSequenceName.ToLower());
          break;
      }

      var TypeGuid = builder.Property(t => t.TypeGuid).IsRequired();

      model.Properties.Add(Id);
      model.Properties.Add(TypeGuid);
      return model;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create object-relational mapping.
    /// </summary>
    /// <param name="connectionSettings">Database connection settings.</param>
    protected EntityTypeToTableMapping(IDatabaseConnectionSettings connectionSettings)
    {
      this.connectionSettings = connectionSettings;
    }

    #endregion
  }
}
