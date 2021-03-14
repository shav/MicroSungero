using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace MicroSungero.Kernel.Domain.Exceptions
{
  /// <summary>
  /// Exception occured while object properties validation.
  /// </summary>
  public class PropertyValidationException : ValidationException
  {
    #region Properties and fields

    /// <summary>
    /// Properties validation errors.
    /// </summary>
    /// <remarks>Grouped into dictionary by property name.</remarks>
    public IDictionary<string, string[]> Errors { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// Initialize properties validation errors of this exception.
    /// </summary>
    /// <param name="errors">Properties validation failures.</param>
    private void SetPropertiesValidationErrors(IEnumerable<ValidationFailure> errors)
    {
      this.Errors = errors
        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
        .ToDictionary(e => e.Key, e => e.ToArray());
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create properties validation exception.
    /// </summary>
    public PropertyValidationException()
      : base()
    {
    }

    /// <summary>
    /// Create properties validation exception.
    /// </summary>
    /// <param name="message">Message.</param>
    public PropertyValidationException(string message)
      : base(message)
    {
      this.Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Create properties validation exception.
    /// </summary>
    /// <param name="errors">Properties validation failures.</param>
    public PropertyValidationException(IEnumerable<ValidationFailure> errors)
    {
      this.SetPropertiesValidationErrors(errors);
    }

    /// <summary>
    /// Create properties validation exception.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="errors">Properties validation failures.</param>
    public PropertyValidationException(string message, IEnumerable<ValidationFailure> errors)
      : this(message)
    {
      this.SetPropertiesValidationErrors(errors);
    }

    #endregion

    #region Object

    public override string ToString()
    {
      var message = new StringBuilder();
      message.AppendLine(this.Message);
      foreach (var propertyError in this.Errors)
      {
        var propertyName = propertyError.Key;
        message.AppendLine($"{propertyName}:");

        foreach (var error in propertyError.Value)
        {
          message.AppendLine($"  * {error}");
        }
      }
      return message.ToString();
    }

    #endregion
  }
}
