using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroSungero.Kernel.API.Validation;

namespace MicroSungero.Kernel.API.Behaviors
{
  /// <summary>
  /// The command pipeline behavior for validation of command arguments.
  /// </summary>
  /// <typeparam name="TCommand">Type of command.</typeparam>
  /// <typeparam name="TResult">Type of command result.</typeparam>
  public class CommandValidationBehaviour<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
    where TCommand : ICommand<TResult>
  {
    /// <summary>
    /// Command validation service.
    /// </summary>
    private readonly ICommandValidationService validationService;

    /// <summary>
    /// Create command validation behavior.
    /// </summary>
    /// <param name="validationService">Command validation service.</param>
    public CommandValidationBehaviour(ICommandValidationService validationService)
    {
      this.validationService = validationService;
    }

    #region IPipelineBehavior

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
    {
      await this.validationService.ValidateAsync<TCommand, TResult>(command, cancellationToken);
      return await next();
    }

    #endregion
  }
}
