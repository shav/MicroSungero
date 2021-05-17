using NLog;

namespace MicroSungero.Common.Logging
{
  /// <summary>
  /// Logs manager for NLog.
  /// </summary>
  public class NLogManager: ILogManager
  {
    #region ILogManager

    public void Flush()
    {
      LogManager.Flush();
    }

    #endregion
  }
}