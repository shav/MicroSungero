namespace MicroSungero.Kernel.Domain
{
  /// <summary>
  /// A small immutable object whose equality is not based on identity,
  /// i.e. two value objects are equal when they have the same value, not necessarily being the same object.
  /// </summary>
  /// <remarks>
  /// See also for more details:
  /// https://en.wikipedia.org/wiki/Value_object
  /// https://martinfowler.com/bliki/ValueObject.html
  /// </remarks>
  public interface IValueObject
  {
  }
}
