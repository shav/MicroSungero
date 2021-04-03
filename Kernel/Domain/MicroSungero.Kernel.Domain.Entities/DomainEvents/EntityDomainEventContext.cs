namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Context that provides access to transactional entities domain events scope.
  /// </summary>
  public class EntityDomainEventContext : IEntityDomainEventContext
  {
    #region IEntityDomainEventsContext

    public IEntityDomainEventTransactionScope Current => EntityDomainEventTransactionScope.Current;

    #endregion
  }
}
