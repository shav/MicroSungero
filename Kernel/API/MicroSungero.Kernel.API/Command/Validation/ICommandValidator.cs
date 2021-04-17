using MicroSungero.Kernel.Domain.Validation;

namespace MicroSungero.Kernel.API.Validation
{
  /// <summary>
  /// Validator that is called before command execution and validates command arguments
  /// </summary>
  /// <typeparam name="TCommand">Type of command.</typeparam>
  /// <typeparam name="TResult">Type of command result/</typeparam>
  public interface ICommandValidator<in TCommand> : IErrorValidator<TCommand>, ICommandValidator
    where TCommand : IBaseCommand
  {
  }

  /// <summary>
  /// Base command validator that is called before command execution.
  /// </summary>
  public interface ICommandValidator
  {
  }
}
