using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MicroSungero.Kernel.API.Validation;

namespace MicroSungero.Kernel.API.Services
{
  /// <summary>
  /// An implementation of service for validation of command arguments.
  /// </summary>
  public class CommandValidationService : ICommandValidationService
  {
    /// <summary>
    /// All registered command validators for all command types.
    /// </summary>
    private readonly IEnumerable<ICommandValidator> validators;

    /// <summary>
    /// Create command validation service.
    /// </summary>
    /// <param name="validators">All registered command validators for command.</param>
    public CommandValidationService(IEnumerable<ICommandValidator> validators)
    {
      this.validators = validators.ToArray();
    }

    #region ICommandValidationService

    public async Task ValidateAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand<TResult>
    {
      var commandValidators = this.validators.OfType<ICommandValidator<TCommand, TResult>>();
      if (commandValidators.Any())
      {
        var validationResults = await Task.WhenAll(commandValidators.Select(v => v.ValidateAsync(command, false, cancellationToken)));
        var errors = validationResults.SelectMany(r => r.Errors).Where(e => e != null).ToList();

        if (errors.Count != 0)
          throw new Kernel.Domain.Exceptions.PropertyValidationException(errors);
      }
    }

    #endregion
  }
}
