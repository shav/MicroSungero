namespace MicroSungero.Common.Logging.NLog
{
  /// <summary>
  /// Log factory.
  /// </summary>
  public class LogFactory: ILogFactory
  {
    #region Properties and fields

    /// <summary>
    /// Logger configuration.
    /// </summary>
    private ILogConfiguration config;

    #endregion

    #region ILogFactory

    public ILog CreateLogger(string loggerName)
    {
      return new NLogWrapper(loggerName, this.config);
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create log factory.
    /// </summary>
    /// <param name="config">Logger configuration.</param>
    public LogFactory(ILogConfiguration config)
    {
      this.config = config;
    }

    #endregion
  }
}
