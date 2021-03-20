using System;

namespace MicroSungero.Data.Exceptions
{
  /// <summary>
  /// An exception occured working with database using the unit-of-work pattern implementation.
  /// </summary>
  public class SessionException : Exception
  {
    /// <summary>
    /// Create session exception.
    /// </summary>
    public SessionException()
    {
    }

    /// <summary>
    /// Create session exception.
    /// </summary>
    /// <param name="message">Error message.</param>
    public SessionException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Create session exception.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="innerException">Original data access exception.</param>
    public SessionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
  }
}
