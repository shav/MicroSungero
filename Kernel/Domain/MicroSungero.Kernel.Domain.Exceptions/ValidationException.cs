using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSungero.Kernel.Domain.Validation;

namespace MicroSungero.Kernel.Domain.Exceptions
{
  /// <summary>
  /// Validation exception.
  /// </summary>
  public class ValidationException : DomainException
  {
    #region Properties and fields

    /// <summary>
    /// Validation errors.
    /// </summary>
    public IEnumerable<IValidationFailure> Errors { get; }

    #endregion

    #region Object

    public override string ToString()
    {
      var message = new StringBuilder();
      message.AppendLine(this.Message);
      foreach (var error in this.Errors)
      {
        message.AppendLine($"  * {error}");
      }
      return message.ToString();
    }

    #endregion

    #region Constructors

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
      this.Errors = Array.Empty<IValidationFailure>();
    }

    /// <summary>
    /// Create validation exception.
    /// </summary>
    /// <param name="errors">Validation errors.</param>
    public ValidationException(IEnumerable<IValidationFailure> errors)
      : base("One or more validation errors have occurred.")
    {
      this.Errors = errors.ToArray();
    }

    #endregion
  }
}
