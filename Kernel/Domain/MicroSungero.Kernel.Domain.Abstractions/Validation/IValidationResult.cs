using System.Collections.Generic;

namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Validation result.
  /// </summary>
  public interface IValidationResult
  {
    /// <summary>
    /// Whether validation succeeded
    /// </summary>
    bool IsValid { get; }

    /// <summary>
    /// A collection of errors.
    /// </summary>    
    IEnumerable<IValidationFailure> Errors { get; }
  }
}
