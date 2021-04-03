namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Context that provides access to transactional entities domain events scope.
  /// </summary>
  public interface IEntityDomainEventContext
  {
    /// <summary>
    /// Current transactional entities domain events scope.
    /// </summary>
    IEntityDomainEventTransactionScope Current { get; }
  }
}
