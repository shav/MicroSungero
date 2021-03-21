using System;

namespace MicroSungero.Data.Exceptions
{
  /// <summary>
  /// An exception occured working with database using the unit-of-work pattern implementation.
  /// </summary>
  public class UnitOfWorkException : Exception
  {
    /// <summary>
    /// Create unit-of-work exception.
    /// </summary>
    public UnitOfWorkException()
    {
    }

    /// <summary>
    /// Create unit-of-work exception.
    /// </summary>
    /// <param name="message">Error message.</param>
    public UnitOfWorkException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Create unit-of-work exception.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Original data access exception.</param>
    public UnitOfWorkException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
