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
    public string TableName => $"{this.ModuleName.Replace('.', '_')}_{typeof(TEntity).Name}";

    /// <summary>
    /// Module name which entity type is defined at.
    /// </summary>
    protected abstract string ModuleName { get; }

    #endregion

    #region IEntityTypeConfiguration

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
      builder.ToTable(this.TableName);

      builder.Ignore(t => ((IPersistentObject)t).IsTransient);
      builder.Ignore(t => ((IPersistentObject)t).IsDeleted);
      builder.Ignore(t => t.DisplayValue);

      builder.HasKey(t => t.Id);
      builder.Property(t => t.Id)
        .UseHiLo($"{this.TableName}_Id")
        .IsRequired();

      builder.Property(t => t.TypeGuid)
        .IsRequired();
    }

    #endregion
  }
}
