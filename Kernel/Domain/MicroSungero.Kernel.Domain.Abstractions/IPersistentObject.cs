namespace MicroSungero.Kernel.Domain
{
  /// <summary>
  /// Object that can be persisted to the storage.
  /// </summary>
  public interface IPersistentObject
  {
    /// <summary>
    /// Object is new and not persisted to the storage yet.
    /// </summary>
    bool IsTransient { get; set; }

    /// <summary>
    /// Object is deleted from the storage.
    /// </summary>
    bool IsDeleted { get; set; }
  }
}
