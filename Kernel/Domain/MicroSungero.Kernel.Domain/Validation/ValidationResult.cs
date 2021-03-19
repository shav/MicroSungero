using System.Collections.Generic;
using System.Linq;

namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Validation result.
  /// </summary>
  public class ValidationResult : IValidationResult
  {
    #region IValidationResult

    public bool IsValid => !this.Errors.Any();

    public IEnumerable<IValidationFailure> Errors { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Create wrapped validation error.
    /// </summary>
    /// <param name="failure">Original validation error.</param>
    /// <returns>Wrapped validation error.</returns>
    private static IValidationFailure CreateValidationError(FluentValidation.Results.ValidationFailure failure)
    {
      if (!string.IsNullOrWhiteSpace(failure.PropertyName))
      {
        return new PropertyValidationFailure(failure);
      }
      return new ValidationFailure(failure);
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create validation result.
    /// </summary>
    /// <param name="errors">Validation errors collection.</param>
    public ValidationResult(IEnumerable<IValidationFailure> errors)
    {
      this.Errors = errors.ToList();
    }

    /// <summary>
    /// Create wrapped validation result.
    /// </summary>
    /// <param name="validationResult">Original validation result.</param>
    public ValidationResult(FluentValidation.Results.ValidationResult validationResult)
    {
      this.Errors = validationResult.Errors.Select(e => CreateValidationError(e)).ToList();
    }

    #endregion
  }
}
