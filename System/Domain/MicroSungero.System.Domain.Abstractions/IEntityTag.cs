namespace MicroSungero.System.Domain
{
  /// <summary>
  /// Entity tag.
  /// </summary>
  public interface IEntityTag
  {
    /// <summary>
    /// Tag name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Tag color.
    /// </summary>
    Color Color { get; }
  }
}
