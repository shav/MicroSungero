using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
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
  {
    /// <summary>
    /// Command validation service.
    /// </summary>
    private readonly ICommandValidationService validationService;

    /// <summary>
    /// All command validators.
    /// </summary>
    private readonly IValidator<TCommand>[] validators;

    /// <summary>
    /// Create command validation behavior.
    /// </summary>
    /// <param name="validationService">Command validation service.</param>
    /// <param name="validators">Command validators.</param>
    public CommandValidationBehaviour(ICommandValidationService validationService, IEnumerable<IValidator<TCommand>> validators)
    {
      this.validationService = validationService;
      this.validators = validators.ToArray();
    }

    #region IPipelineBehavior

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
    {
      if (command is ICommand<TResult>)
      {
        var behaviorType = typeof(CommandValidationBehaviourImpl<,>).MakeGenericType(typeof(TCommand), typeof(TResult));
        var behavior = (IPipelineBehavior<TCommand, TResult>)Activator.CreateInstance(behaviorType, this.validationService, this.validators);
        await behavior.Handle(command, cancellationToken, next);
      }
      return await next();
    }

    #endregion
  }

  /// <summary>
  /// The implementation of command pipeline behavior for validation of command arguments.
  /// </summary>
  /// <typeparam name="TCommand">Type of command.</typeparam>
  /// <typeparam name="TResult">Type of command result.</typeparam>
  internal class CommandValidationBehaviourImpl<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
    where TCommand : ICommand<TResult>
  {
    /// <summary>
    /// Command validation service.
    /// </summary>
    private readonly ICommandValidationService validationService;

    /// <summary>
    /// Create implementation of command validation behavior.
    /// </summary>
    /// <param name="validationService">Command validation service.</param>
    /// <param name="validators">Command validators.</param>
    public CommandValidationBehaviourImpl(ICommandValidationService validationService, IEnumerable<IValidator<TCommand>> validators)
    {
      this.validationService = validationService;
      this.validationService.AddValidators(validators.OfType<ICommandValidator<TCommand>>().ToArray());
    }

    #region IPipelineBehavior

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
    {
      await this.validationService.ValidateAsync(command, cancellationToken);
      return await next();
    }

    #endregion
  }
}
