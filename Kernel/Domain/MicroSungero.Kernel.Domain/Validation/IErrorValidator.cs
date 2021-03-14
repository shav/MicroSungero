using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Object validator that can throw validation exceptions.
  /// </summary>
  /// <typeparam name="T">Type of objects that are validated by particular implementation of validator.</typeparam>
  public interface IErrorValidator<T>
  {
    /// <summary>
    /// Validate object.
    /// </summary>
    /// <param name="instance">Object instance.</param>
    /// <param name="throwOnErrors">Should throw exception on validation error?</param>
    /// <returns>Validation result.</returns>
    ValidationResult Validate(T instance, bool throwOnErrors);

    /// <summary>
    /// Async validate object.
    /// </summary>
    /// <param name="instance">Object instance.</param>
    /// <param name="throwOnErrors">Should throw exception on validation error?</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Validation result.</returns>
    Task<ValidationResult> ValidateAsync(T instance, bool throwOnErrors, CancellationToken cancellation = default);
  }
}
