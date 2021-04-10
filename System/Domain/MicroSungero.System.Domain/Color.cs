using MicroSungero.Kernel.Domain;

namespace MicroSungero.System.Domain
{
  /// <summary>
  /// Color.
  /// </summary>
  public class Color: Enumeration, IColor
  {
    #region Enumeration items

    public static readonly Color Red = new Color(nameof(Red));

    public static readonly Color Orange = new Color(nameof(Orange));

    public static readonly Color Yellow = new Color(nameof(Yellow));

    public static readonly Color Green = new Color(nameof(Green));

    public static readonly Color Blue = new Color(nameof(Blue));

    public static readonly Color Purple = new Color(nameof(Purple));

    #endregion

    #region IColor

    string IColor.Name => this.Value;

    #endregion

    #region Constructors

    /// <summary>
    /// Create color.
    /// </summary>
    /// <param name="name">The color name.</param>
    public Color(string name)
      : base(name)
    {
    }

    #endregion
  }
}
