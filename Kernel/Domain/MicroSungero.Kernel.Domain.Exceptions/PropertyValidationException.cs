using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSungero.Kernel.Domain.Validation;

namespace MicroSungero.Kernel.Domain.Exceptions
{
  /// <summary>
  /// Exception occured while object properties validation.
  /// </summary>
  public class PropertyValidationException : Exception
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
    private void SetPropertiesValidationErrors(IEnumerable<IValidationFailure> errors)
    {
      this.Errors = errors
        .GroupBy(e => (e as IPropertyValidationFailure)?.PropertyName ?? string.Empty, e => e.ErrorMessage)
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
      this.Errors = new Dictionary<string, string[]>();
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
    public PropertyValidationException(IEnumerable<IValidationFailure> errors)
    {
      this.SetPropertiesValidationErrors(errors);
    }

    /// <summary>
    /// Create properties validation exception.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="errors">Properties validation failures.</param>
    public PropertyValidationException(string message, IEnumerable<IValidationFailure> errors)
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
        if (!string.IsNullOrWhiteSpace(propertyName))
          message.AppendLine($"{propertyName}:");
        else if (this.Errors.Keys.Any(p => !string.IsNullOrWhiteSpace(p)))
          message.AppendLine("Other validation errors:");

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
