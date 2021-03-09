using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MicroSungero.Kernel.API
{
  /// <summary>
  /// A handler that retrieves data for query and returns it to the caller.
  /// </summary>
  /// <typeparam name="TQuery">Query type.</typeparam>
  /// <typeparam name="TResult">Query result type.</typeparam>
  public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
  {
    /// <summary>
    /// Retrieve data for the query.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Retrieved data.</returns>
    Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
  }
}
