namespace MicroSungero.Data
{
  /// <summary>
  /// Change tracking entry for the persistent record.
  /// </summary>
  /// <typeparam name="TRecord">Type of persistent record.</typeparam>
  public interface IRecordEntry<TRecord>
  {
    /// <summary>
    /// Persistent record.
    /// </summary>
    TRecord Record { get; }

    /// <summary>
    /// Change tracking state of the record.
    /// </summary>
    RecordState State { get; set; }
  }
}
