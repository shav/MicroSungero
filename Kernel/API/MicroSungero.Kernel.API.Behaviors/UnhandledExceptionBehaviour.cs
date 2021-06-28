using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroSungero.Common.Logging;

namespace MicroSungero.Kernel.API.Behaviors
{
  /// <summary>
  /// Pipeline behavior for logging unhandled exceptions.
  /// </summary>
  /// <typeparam name="TRequest">Type of request.</typeparam>
  /// <typeparam name="TResponse">Type of response.</typeparam>
  public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
    /// <summary>
    /// Logger.
    /// </summary>
    private static readonly ILog log = Logs.GetLogger("UnhandledExceptionLog");

    /// <summary>
    /// Create unhandled exception behavior.
    /// </summary>
    public UnhandledExceptionBehaviour()
    {
    }

    #region IPipelineBehavior

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      try
      {
        return await next();
      }
      catch (Exception ex)
      {
        var requestName = typeof(TRequest).Name;
        log.Error(ex, "Unhandled exception occured for request {Name} {@Request}", requestName, request);
        throw;
      }
    }

    #endregion
  }
}
