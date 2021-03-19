using System;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Base child entity implementation.
  /// </summary>
  public abstract class ChildEntity : Entity, IChildEntity, IInternalChildEntity
  {
    #region Constants

    /// <summary>
    /// Base child entity type identifier.
    /// </summary>
    public static new readonly Guid ClassTypeGuid = Guid.Parse("78B71316-3F5E-484D-B53B-F7F98E85C93F");

    #endregion

    #region IChildEntity

    public virtual IEntity RootEntity { get; set; }

    #endregion

    #region IEntity

    public override Guid TypeGuid => ClassTypeGuid;

    #endregion

    #region Constructors

    protected ChildEntity()
    {
    }

    #endregion
  }
}
