using MediatR;

namespace MicroSungero.Kernel.API
{
  /// <summary>
  /// A command that performs an action (may cause side effects) and returns action result data to the caller.
  /// </summary>
  /// <typeparam name="TResult">Command result type.</typeparam>
  /// <remarks>
  /// See more for details:
  /// https://en.wikipedia.org/wiki/Command%E2%80%93query_separation
  /// https://microservices.io/patterns/data/cqrs.html
  /// </remarks>
  public interface ICommand<out TResult> : IRequest<TResult>
  {
  }

  /// <summary>
  /// A command that performs an action (may cause side effects) and don't return any result to the caller.
  /// </summary>
  /// <remarks>
  /// See more for details:
  /// https://en.wikipedia.org/wiki/Command%E2%80%93query_separation
  /// https://microservices.io/patterns/data/cqrs.html
  /// </remarks>
  public interface ICommand : IRequest
  {
  }
}
