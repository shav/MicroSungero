using MediatR;

namespace MicroSungero.Kernel.API
{
  /// <summary>
  /// A query that only returns data to the caller, without updating data and other side effects.
  /// </summary>
  /// <typeparam name="TResult">Query result type.</typeparam>
  /// <remarks>
  /// See more for details:
  /// https://en.wikipedia.org/wiki/Command%E2%80%93query_separation
  /// https://microservices.io/patterns/data/cqrs.html
  /// </remarks>
  public interface IQuery<out TResult> : IRequest<TResult>
  {
  }
}
