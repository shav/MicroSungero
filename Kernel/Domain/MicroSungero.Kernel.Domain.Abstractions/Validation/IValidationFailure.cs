namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Validation failure.
  /// </summary>
  public interface IValidationFailure
  {
    /// <summary>
    /// Error message.
    /// </summary>
    string ErrorMessage { get; }

    /// <summary>
    /// Custom severity level associated with the failure.
    /// </summary>
    Severity Severity { get; }

    /// <summary>
    /// Error code.
    /// </summary>
    string ErrorCode { get; }
  }
}
