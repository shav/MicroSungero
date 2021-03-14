using System.Threading.Tasks;
using MediatR;

namespace MicroSungero.Kernel.API.Services
{
  /// <summary>
  /// An implementation of the service that executes queries.
  /// </summary>
  public class QueryService : IQueryService
  {
    /// <summary>
    /// Queries sender to the handlers pipeline.
    /// </summary>
    private readonly ISender querySender;

    /// <summary>
    /// Create query service.
    /// </summary>
    /// <param name="querySender">Queries sender to the handlers pipeline.</param>
    public QueryService(ISender querySender)
    {
      this.querySender = querySender;
    }

    #region IQueryService

    public Task<T> Execute<T>(IQuery<T> query)
    {
      return this.querySender.Send(query);
    }

    #endregion
  }
}
