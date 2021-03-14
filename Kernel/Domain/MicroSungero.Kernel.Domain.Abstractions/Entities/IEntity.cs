using System;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Entity is an object of domain area whose instances are not fundamentally defined by their attributes,
  /// but rather by a thread of continuity and identity (ususally identity is surrogate).
  /// </summary>
  /// <remarks>
  /// If two instances of the same object have different attribute values, but same identity value, they are the same entity.
  /// </remarks>
  public interface IEntity
  {
    /// <summary>
    /// Entity identifier.
    /// </summary>
    /// <remarks>
    /// Entity identifier is unique within the type of entities.
    /// </remarks>
    int Id { get; }

    /// <summary>
    /// Entity type identifier.
    /// </summary>
    /// <remarks>
    /// Entity type identifier is unique within the whole application.
    /// </remarks>
    Guid TypeGuid { get; }
  }

  /// <summary>
  /// An internal interface of entity that provides extended access to the entity.
  /// </summary>
  public interface IInternalEntity : IEntity
  {
    /// <summary>
    /// Entity identifier (allows to set the identifier value).
    /// </summary>
    int Id { get; set; }
  }
}
