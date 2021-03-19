namespace MicroSungero.Kernel.Domain
{
  /// <summary>
  /// An named item of complete ended set of items.
  /// Also used as a container for the set of named enumerated items.
  /// </summary>
  /// <remarks>
  /// See also for more details:
  /// https://en.wikipedia.org/wiki/Enumerated_type
  /// </remarks>
  public interface IEnumeration
  {
    /// <summary>
    /// The value of enumeration item.
    /// </summary>
    string Value { get; }
  }
}
