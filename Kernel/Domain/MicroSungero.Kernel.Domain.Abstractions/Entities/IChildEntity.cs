namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// The entity whose lifecycle is entirely dependent on another entity (which is called root or parent entity).
  /// </summary>
  /// <remarks>
  /// It’s a strong relationship: the Child Entity has only meaning in the context of its parent, and cannot exist outside of it.
  /// If the Parent Entity ceases to exist, the Child Entity is deleted as well.
  /// Ideally, all operations on the child are handled by the parent, even its creation.
  /// </remarks>
  public interface IChildEntity : IEntity
  {
    /// <summary>
    /// Parent entity.
    /// </summary>
    IEntity RootEntity { get; }
  }

  /// <summary>
  /// An internal interface of child entity that provides extended access.
  /// </summary>
  public interface IInternalChildEntity : IChildEntity
  {
    /// <summary>
    /// Parent entity (allows to set parent entity).
    /// </summary>
    IEntity RootEntity { get; set; }
  }
}
