using System.Collections.Generic;
using MicroSungero.Kernel.Domain;

namespace MicroSungero.System.Domain
{
  /// <summary>
  /// Entity tag.
  /// </summary>
  public class EntityTag : ValueObject, IEntityTag
  {
    #region IEntityTag

    public string Name { get; private set; }

    public IColor Color { get; private set; }

    #endregion

    #region ValueObject

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return this.Name;
      yield return this.Color;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create entity tag.
    /// </summary>
    /// <param name="name">Tag name.</param>
    /// <param name="color">Tag color.</param>
    public EntityTag(string name, IColor color)
    {
      this.Name = name;
      this.Color = color;
    }

    #endregion
  }
}
