using System;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base entity implementation.
  /// </summary>
  public abstract class Entity : IEntity, IInternalEntity
  {
    #region Constants

    /// <summary>
    /// Base entity type identifier.
    /// </summary>
    public static readonly Guid ClassTypeGuid = Guid.Parse("79AAA247-5F24-47A3-BF05-F0CD7AD30161");

    #endregion

    #region IEntity

    public virtual int Id { get; set; }

    public virtual Guid TypeGuid
    {
      get => ClassTypeGuid;
      private set { } // HACK: Setter is used by ORM
    }

    public virtual string DisplayValue => this.ToString();

    #endregion

    #region Object

    public override bool Equals(object obj)
    {
      var other = obj as Entity;
      if (other == null)
        return false;

      return this.Id == other.Id && this.TypeGuid == other.TypeGuid;
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return 397 * this.Id.GetHashCode() + this.TypeGuid.GetHashCode();
      }
    }

    public override string ToString()
    {
      return $"{{{nameof(this.TypeGuid)}: {this.TypeGuid}, {nameof(Id)}: {this.Id}}}";
    }

    #endregion

    #region Constructors

    protected Entity()
    {
    }

    #endregion
  }
}
