using System;

namespace MicroSungero.Kernel.Domain.Exceptions
{
  /// <summary>
  /// The kind of exceptions that is generated when some object is not found.
  /// </summary>
  public class ObjectNotFoundException : DomainException
  {
    /// <summary>
    /// Create exception when object is not found.
    /// </summary>
    public ObjectNotFoundException() : base()
    {
    }

    /// <summary>
    /// Create exception when object is not found.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public ObjectNotFoundException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Create exception when object is not found.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Original exception.</param>
    public ObjectNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Create exception when object is not found.
    /// </summary>
    /// <param name="key">Unique object key.</param>
    public ObjectNotFoundException(object key)
      : this(string.Empty, key)
    {
    }

    /// <summary>
    /// Create exception when object is not found.
    /// </summary>
    /// <param name="name">Object name (or maybe object type name).</param>
    /// <param name="key">Unique object key.</param>
    public ObjectNotFoundException(string name, object key)
      : this("Object" + (!string.IsNullOrWhiteSpace(name) ? $" \"{name}\"" : string.Empty) + $" ({key}) was not found.")
    {
    }
  }
}
