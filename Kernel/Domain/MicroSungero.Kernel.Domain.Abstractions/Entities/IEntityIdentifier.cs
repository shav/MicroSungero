using System;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Unique entity identifier within the whole application.
  /// </summary>
  public interface IEntityIdentifier
  {
    /// <summary>
    /// Entity type identifier.
    /// </summary>
    Guid TypeGuid { get; set; }

    /// <summary>
    /// Entity surrogate identifier.
    /// </summary>
    int Id { get; set; }
  }
}
