namespace MicroSungero.Kernel.Domain.Exceptions
{
  /// <summary>
  /// Validation exception.
  /// </summary>
  public class ValidationException : DomainException
  {
    /// <summary>
    /// Create validation exception.
    /// </summary>
    public ValidationException()
      : this("One or more validation errors have occurred.")
    {
    }

    /// <summary>
    /// Create validation exception.
    /// </summary>
    /// <param name="message">Message.</param>
    public ValidationException(string message)
      : base(message)
    {
    }
  }
}
