using System.Collections.Generic;
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
    /// <param name="command">The command to validate arguments.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task ValidateAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : IBaseCommand;

    /// <summary>
    /// Add validators for command.
    /// </summary>
    /// <typeparam name="TCommand">Type of command for validation.</typeparam>
    /// <param name="validators">Command validators.</param>
    void AddValidators<TCommand>(IEnumerable<ICommandValidator<TCommand>> validators) where TCommand : IBaseCommand;
  }
}
