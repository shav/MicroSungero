using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Data.EntityFramework
{
  /// <summary>
  /// Base object-relational mapping for child entity type to database table.
  /// </summary>
  /// <typeparam name="TChildEntity">Type of child entity.</typeparam>
  public abstract class ChildEntityTypeToTableMapping<TChildEntity> : EntityTypeToTableMapping<TChildEntity>
    where TChildEntity : ChildEntity
  {
    public override void Configure(EntityTypeBuilder<TChildEntity> builder)
    {
      base.Configure(builder);
      builder.Ignore(t => t.RootEntity);
    }
  }
}
