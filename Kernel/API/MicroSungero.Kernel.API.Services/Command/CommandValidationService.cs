using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    private readonly List<ICommandValidator> validators = new List<ICommandValidator>();

    /// <summary>
    /// Create command validation service.
    /// </summary>
    public CommandValidationService()
    {
    }

    #region ICommandValidationService

    public async Task ValidateAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : IBaseCommand
    {
      var commandValidators = this.validators.OfType<ICommandValidator<TCommand>>();
      if (commandValidators.Any())
      {
        var validationResults = await Task.WhenAll(commandValidators.Select(v => v.ValidateAsync(command, false, cancellationToken)));
        var errors = validationResults.SelectMany(r => r.Errors).Where(e => e != null).ToList();

        if (errors.Count != 0)
          throw new Kernel.Domain.Exceptions.PropertyValidationException(errors);
      }
    }

    public void AddValidators<TCommand>(IEnumerable<ICommandValidator<TCommand>> validators) where TCommand : IBaseCommand
    {
      foreach (var validator in validators)
      {
        if (!this.validators.Contains(validator))
          this.validators.Add(validator);
      }
    }

    #endregion
  }
}
