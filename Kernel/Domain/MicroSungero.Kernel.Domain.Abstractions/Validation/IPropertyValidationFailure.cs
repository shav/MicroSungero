namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Property validation failure.
  /// </summary>
  public interface IPropertyValidationFailure: IValidationFailure
  {
    /// <summary>
    /// Property name.
    /// </summary>
    string PropertyName { get; }

    /// <summary>
    /// The property value that caused the failure.
    /// </summary>
    object AttemptedValue { get; }
  }
}
