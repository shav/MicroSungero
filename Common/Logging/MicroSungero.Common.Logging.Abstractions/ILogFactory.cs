namespace MicroSungero.Common.Logging
{
  /// <summary>
  /// Log factory.
  /// </summary>
  public interface ILogFactory
  {
    /// <summary>
    /// Create logger.
    /// </summary>
    /// <param name="loggerName">Logger name.</param>
    /// <returns>Logger.</returns>
    ILog CreateLogger(string loggerName);
  }
}
