using System;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Unique entity identifier within the whole application.
  /// </summary>
  [Serializable]
  public struct EntityIdentifier : IEntityIdentifier, IEquatable<EntityIdentifier>
  {
    #region Constants

    /// <summary>
    /// Special identifier for unknown entities.
    /// </summary>
    public const int UnknownEntityId = 0;

    #endregion

    #region IEntityIdentifier

    public Guid TypeGuid { get; set; }

    public int Id { get; set; }

    #endregion

    #region Properties and fields

    private static readonly EntityIdentifier empty = new EntityIdentifier(Guid.Empty, UnknownEntityId);

    /// <summary>
    /// Empty identifier (for unknown entities).
    /// </summary>
    public static EntityIdentifier Empty => empty;

    #endregion

    #region Methods

    /// <summary>
    /// Check if the identifier references to the passed entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <returns>True if the identifier references to the passed entity, else False.</returns>
    public bool ReferencesTo(IEntity entity)
    {
      return entity != null && entity.Id == this.Id && entity.TypeGuid == this.TypeGuid;
    }

    /// <summary>
    /// Check if the identifier is null or empty (references to unknown entity).
    /// </summary>
    /// <param name="entityIdentifier">Entity identifier to check.</param>
    /// <returns>True if the identifier is null or empty, else False.</returns>
    public static bool IsNullOrEmpty(EntityIdentifier entityIdentifier)
    {
      return entityIdentifier == null || entityIdentifier.Equals(EntityIdentifier.Empty);
    }

    #endregion

    #region Object

    public override bool Equals(object obj)
    {
      if (!(obj is EntityIdentifier))
        return false;

      return this.Equals((EntityIdentifier)obj);
    }

    public static bool operator ==(EntityIdentifier entityIdentifier1, EntityIdentifier entityIdentifier2)
    {
      return entityIdentifier1.Equals(entityIdentifier2);
    }

    public static bool operator !=(EntityIdentifier entityIdentifier1, EntityIdentifier entityIdentifier2)
    {
      return !entityIdentifier1.Equals(entityIdentifier2);
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
      return $"{{{nameof(this.TypeGuid)}: {this.TypeGuid}, {nameof(this.Id)}: {this.Id}}}";
    }

    #endregion

    #region IEquatable<T>

    public bool Equals(EntityIdentifier other)
    {
      return this.Id == other.Id && this.TypeGuid == other.TypeGuid;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create entity unique identifier.
    /// </summary>
    /// <param name="entity">Entity which identifier is creating for.</param>
    public EntityIdentifier(IEntity entity)
      : this(entity?.TypeGuid ?? Guid.Empty, entity?.Id ?? UnknownEntityId) { }

    /// <summary>
    /// Create entity unique identifier.
    /// </summary>
    /// <param name="entityTypeGuid">Entity type identifier.</param>
    /// <param name="entityId">Entity surrogate identifier.</param>
    public EntityIdentifier(Guid entityTypeGuid, int entityId)
    {
      this.TypeGuid = entityTypeGuid;
      this.Id = entityId;
    }

    #endregion
  }
}
