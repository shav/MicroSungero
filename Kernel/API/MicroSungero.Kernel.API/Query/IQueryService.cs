using System.Threading.Tasks;

namespace MicroSungero.Kernel.API
{
  /// <summary>
  /// A service that executes queries via registered query handlers at any point of application.
  /// </summary>
  public interface IQueryService
  {
    /// <summary>
    /// Execute the query and return retrieved data to the caller.
    /// </summary>
    /// <typeparam name="TResult">Query result type.</typeparam>
    /// <param name="query">Query.</param>
    /// <returns>Retrieved data.</returns>
    Task<TResult> Execute<TResult>(IQuery<TResult> query);
  }
}
