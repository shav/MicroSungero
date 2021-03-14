using System.Threading;
using System.Threading.Tasks;

namespace MicroSungero.Kernel.API.Validation
{
  /// <summary>
  /// A service for validation of command arguments.
  /// </summary>
  public interface ICommandValidationService
  {
    /// <summary>
    /// Async validate command arguments.
    /// </summary>
    /// <typeparam name="TCommand">Type of command.</typeparam>
    /// <typeparam name="TResult">Type of command result/</typeparam>
    /// <param name="command">The command to validate arguments.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task ValidateAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand<TResult>;
  }
}
