using System.Threading.Tasks;
using MediatR;
using MicroSungero.Kernel.API;

namespace MicroSungero.Kernel.Infrastructure.Services
{
  /// <summary>
  /// An implementation of the service that executes commands.
  /// </summary>
  public class CommandService : ICommandService
  {
    /// <summary>
    /// Commands sender to the handlers pipeline.
    /// </summary>
    private readonly ISender commandSender;

    /// <summary>
    /// Create command service.
    /// </summary>
    /// <param name="commandSender">Commands sender to the handlers pipeline.</param>
    public CommandService(ISender commandSender)
    {
      this.commandSender = commandSender;
    }

    #region ICommandService

    public Task<T> Execute<T>(ICommand<T> command)
    {
      return this.commandSender.Send(command);
    }

    public Task Execute(ICommand command)
    {
      return this.commandSender.Send(command);
    }

    #endregion
  }
}
