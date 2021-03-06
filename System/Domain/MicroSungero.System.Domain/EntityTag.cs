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

    public Color Color { get; private set; }

    #endregion

    #region ValueObject

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return this.Name;
      yield return this.Color;
    }

    #endregion

    #region Object

    public override string ToString()
    {
      var str = string.Empty;
      if (!string.IsNullOrWhiteSpace(this.Name))
        str += this.Name;

      if (!string.IsNullOrWhiteSpace(this.Color?.Value))
        str += $", {nameof(Color)}: {this.Color}}}";

      return str;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create entity tag.
    /// </summary>
    /// <param name="name">Tag name.</param>
    /// <param name="color">Tag color.</param>
    public EntityTag(string name, Color color)
    {
      this.Name = name;
      this.Color = color;
    }

    /// <summary>
    /// Create empty entity tag.
    /// </summary>
    public EntityTag()
    {
    }

    #endregion
  }
}
