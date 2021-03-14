using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace MicroSungero.Kernel.Domain.Validation
{
  /// <summary>
  /// Base class for object validation that can throw validation exceptions.
  /// </summary>
  /// <typeparam name="T">Type of objects that are validated by particular implementation of base validator.</typeparam>
  public abstract class BaseErrorValidator<T> : AbstractValidator<T>, IErrorValidator<T>, IValidator<T>
  {
    #region IErrorValidator

    public ValidationResult Validate(T instance, bool throwOnErrors)
    {
      if (throwOnErrors)
        return base.Validate(ValidationContext<T>.CreateWithOptions(instance, s => s.ThrowOnFailures()));
      else
        return base.Validate(instance);
    }

    public Task<ValidationResult> ValidateAsync(T instance, bool throwOnErrors, CancellationToken cancellation = default)
    {
      if (throwOnErrors)
        return base.ValidateAsync(ValidationContext<T>.CreateWithOptions(instance, s => s.ThrowOnFailures()), cancellation);
      else
        return base.ValidateAsync(instance);
    }

    #endregion
  }
}
