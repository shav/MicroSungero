using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Base class for object validation that can throw validation exceptions.
  /// </summary>
  /// <typeparam name="T">Type of objects that are validated by particular implementation of base validator.</typeparam>
  public abstract class BaseErrorValidator<T> : AbstractValidator<T>, IErrorValidator<T>, IValidator<T>
  {
    #region IErrorValidator

    public IValidationResult Validate(T instance, bool throwOnErrors)
    {
      if (throwOnErrors)
        return new ValidationResult(base.Validate(ValidationContext<T>.CreateWithOptions(instance, s => s.ThrowOnFailures())));
      else
        return new ValidationResult(base.Validate(instance));
    }

    public async Task<IValidationResult> ValidateAsync(T instance, bool throwOnErrors, CancellationToken cancellation = default)
    {
      if (throwOnErrors)
        return new ValidationResult(await base.ValidateAsync(ValidationContext<T>.CreateWithOptions(instance, s => s.ThrowOnFailures()), cancellation));
      else
        return new ValidationResult(await base.ValidateAsync(instance));
    }

    #endregion
  }
}
