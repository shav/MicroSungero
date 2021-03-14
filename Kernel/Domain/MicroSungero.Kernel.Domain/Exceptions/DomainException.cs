using System;

namespace MicroSungero.Kernel.Domain.Exceptions
{
  /// <summary>
  /// Base exception type for domain exceptions.
  /// </summary>
  public class DomainException : Exception
  {
    /// <summary>
    /// Create domain exception.
    /// </summary>
    public DomainException()
    {
    }

    /// <summary>
    /// Create domain exception.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public DomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Create domain exception.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Original system exception.</param>
    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
  }
}
