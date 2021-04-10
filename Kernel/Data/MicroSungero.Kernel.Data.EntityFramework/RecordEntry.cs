using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MicroSungero.Kernel.Data.EntityFramework
{
  /// <summary>
  /// Change tracking entry for the persistent record.
  /// </summary>
  /// <typeparam name="TRecord">Type of persistent record.</typeparam>
  public class RecordEntry<TRecord> : IRecordEntry<TRecord> where TRecord : class
  {
    #region Properties and fields

    /// <summary>
    /// Original change tracking entry from EntityFramework DbContext.
    /// </summary>
    private readonly EntityEntry<TRecord> entry;

    #endregion

    #region IRecordEntry

    public TRecord Record => this.entry.Entity;

    public RecordState State
    {
      get { return (RecordState)this.entry.State; }
      set { this.entry.State = (EntityState)value; }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Wrap change tracking entry.
    /// </summary>
    /// <param name="entry">Original change tracking entry from EntityFramework DbContext.</param>
    public RecordEntry(EntityEntry<TRecord> entry)
    {
      this.entry = entry;
    }

    #endregion
  }
}
