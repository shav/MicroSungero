using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MicroSungero.Kernel.API
{
  /// <summary>
  /// A certain command handler that executes the command and returns to the caller task calculating result data.
  /// </summary>
  /// <typeparam name="TCommand">Command type.</typeparam>
  /// <typeparam name="TResult">Command result type.</typeparam>
  public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
  {
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task calculating the command result data.</returns>
    Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
  }

  /// <summary>
  /// A certain command handler that executes the command and returns to the caller executing task.
  /// </summary>
  /// <typeparam name="TCommand">Command type.</typeparam>
  public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
  {
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task executing the command.</returns>
    Task<Unit> Handle(TCommand command, CancellationToken cancellationToken);
  }
}
