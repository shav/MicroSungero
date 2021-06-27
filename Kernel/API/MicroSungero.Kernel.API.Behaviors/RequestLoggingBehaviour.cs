using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroSungero.Common.Logging;

namespace MicroSungero.Kernel.API.Behaviors
{
  /// <summary>
  /// Pipeline behavior for requests logging.
  /// </summary>
  /// <typeparam name="TRequest">Type of request.</typeparam>
  /// <typeparam name="TResponse">Type of response.</typeparam>
  public class RequestLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
    /// <summary>
    /// Perfomance timer.
    /// </summary>
    private readonly Stopwatch timer;

    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = Logs.GetLogger("RequestLog");

    /// <summary>
    /// Create requests logging behavior.
    /// </summary>
    public RequestLoggingBehaviour()
    {
      timer = new Stopwatch();
    }

    #region IPipelineBehavior

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      var requestName = typeof(TRequest).Name;
      log.Info($">> Start request: {requestName} {{@Request}}", request);

      timer.Start();
      TResponse response;
      long elapsedMilliseconds = -1;
      try
      {
        try
        {
          response = await next();
        }
        finally
        {
          timer.Stop();
          elapsedMilliseconds = timer.ElapsedMilliseconds;
        }
        log.Info($"<< Done in {elapsedMilliseconds} ms request: {requestName} {{@Request}}", request);
        return response;
      }
      catch
      {
        log.Info($"<< Failed in {elapsedMilliseconds} ms request: {requestName} {{@Request}}", request);
        throw;
      }
    }

    #endregion
  }
}
