namespace MicroSungero.Common.Logging
{
  /// <summary>
  /// Logs manager.
  /// </summary>
  public interface ILogManager
  {
    /// <summary>
    /// Flush cached messages to log.
    /// </summary>
    void Flush();
  }
}
