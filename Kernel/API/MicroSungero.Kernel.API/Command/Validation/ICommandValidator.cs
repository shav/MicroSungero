using MediatR;
using MicroSungero.Kernel.Domain.Validation;

namespace MicroSungero.Kernel.API.Validation
{
  /// <summary>
  /// Validator that is called before command execution and validates command arguments
  /// </summary>
  /// <typeparam name="TCommand">Type of command.</typeparam>
  /// <typeparam name="TResult">Type of command result/</typeparam>
  public interface ICommandValidator<in TCommand, TResult> : IErrorValidator<TCommand>, ICommandValidator
    where TCommand : ICommand<TResult>
  {
  }

  /// <summary>
  /// Validator that is called before command execution and validates command arguments
  /// </summary>
  /// <typeparam name="TCommand">Type of command.</typeparam>
  public interface ICommandValidator<in TCommand> : ICommandValidator<TCommand, Unit>, ICommandValidator
    where TCommand : ICommand<Unit>
  {
  }

  /// <summary>
  /// Marker interface for validator that is called before command execution.
  /// </summary>
  public interface ICommandValidator
  {
  }
}
