namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Validation failure.
  /// </summary>
  public class ValidationFailure : IValidationFailure
  {
    #region IValidationFailure

    public string ErrorMessage { get; protected set; }

    public Severity Severity { get; protected set; }

    public string ErrorCode { get; protected set; }

    #endregion

    #region Object

    public override string ToString()
    {
      return this.ErrorMessage;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create validation failure.
    /// </summary>
    /// <param name="failure">Original validation failure.</param>
    public ValidationFailure(FluentValidation.Results.ValidationFailure failure)
    {
      this.ErrorMessage = failure.ErrorMessage;
      this.Severity = (Severity)failure.Severity;
      this.ErrorCode = failure.ErrorCode;
    }

    /// <summary>
    /// Create validation failure.
    /// </summary>
    /// <param name="errorMessage">Error message.</param>
    /// <param name="severity">Severity level.</param>
    /// <param name="errorCode">Error code.</param>
    public ValidationFailure(string errorMessage, Severity severity, string errorCode)
    {
      this.ErrorMessage = errorMessage;
      this.Severity = severity;
      this.ErrorCode = errorCode;
    }

    /// <summary>
    /// Default constructor for inherited classes.
    /// </summary>
    protected ValidationFailure()
    {
    }

    #endregion
  }
}
