namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Property validation failure.
  /// </summary>
  public class PropertyValidationFailure : ValidationFailure, IPropertyValidationFailure
  {
    #region IPropertyValidationFailure

    public string PropertyName { get; }

    public object AttemptedValue { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Create property validation failure.
    /// </summary>
    /// <param name="failure">Original validation failure.</param>
    public PropertyValidationFailure(FluentValidation.Results.ValidationFailure failure)
      : base(failure)
    {
      this.PropertyName = failure.PropertyName;
      this.AttemptedValue = failure.AttemptedValue;
    }

    /// <summary>
    /// Create property validation failure.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <param name="errorMessage">Error message.</param>
    public PropertyValidationFailure(string propertyName, string errorMessage)
    {
      this.PropertyName = propertyName;
      this.ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Create property validation failure.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <param name="errorMessage">Error message.</param>
    /// <param name="attemptedValue">The property value that caused the failure.</param>
    public PropertyValidationFailure(string propertyName, string errorMessage, object attemptedValue)
      : this(propertyName, errorMessage)
    {
      this.AttemptedValue = attemptedValue;
    }

    /// <summary>
    /// Create property validation failure.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <param name="errorMessage">Error message.</param>
    /// <param name="attemptedValue">The property value that caused the failure.</param>
    /// <param name="severity">Severity level.</param>
    /// <param name="errorCode">Error code.</param>
    public PropertyValidationFailure(string propertyName, string errorMessage, object attemptedValue, Severity severity, string errorCode)
      : this(propertyName, errorMessage)
    {
      this.AttemptedValue = attemptedValue;
      this.Severity = severity;
      this.ErrorCode = errorCode;
    }

    #endregion
  }
}
