using System;
using MicroSungero.Kernel.Domain.Exceptions;

namespace MicroSungero.System.Domain.Exceptions
{
  /// <summary>
  /// Base domain exception for system module.
  /// </summary>
  public class SystemDomainException : DomainException
  {
    /// <summary>
    /// Create domain exception for system module.
    /// </summary>
    public SystemDomainException()
    {
    }

    /// <summary>
    /// Create domain exception for system module.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public SystemDomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Create domain exception for system module.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Original exception.</param>
    public SystemDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
  }
}
