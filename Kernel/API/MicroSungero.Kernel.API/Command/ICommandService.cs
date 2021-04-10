using System.Threading.Tasks;

namespace MicroSungero.Kernel.API
{
  /// <summary>
  /// A service that executes commands via registered command handlers at any point of application.
  /// </summary>
  public interface ICommandService
  {
    /// <summary>
    /// Executes the command and return to the caller the task calculating the command result data.
    /// </summary>
    /// <typeparam name="TResult">Command result type.</typeparam>
    /// <param name="command">The command to execute.</param>
    /// <returns>The command result data.</returns>
    Task<TResult> Execute<TResult>(ICommand<TResult> command);

    /// <summary>
    /// Executes the command and return to the caller the task executing the command.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <returns>The task executing the command.</returns>
    Task Execute(ICommand command);
  }
}
