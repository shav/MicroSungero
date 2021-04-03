namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Transactional entities domain events scope.
  /// Accumulates entities domain events within current transaction and raises them only after transaction successfully completed.
  /// </summary>
  public interface IEntityDomainEventTransactionScope
  {
    /// <summary>
    /// Add delayed entity domain event to scope within current transaction.
    /// </summary>
    /// <param name="domainEvent"></param>
    void AddEvent(IEntityDomainEvent domainEvent);

    /// <summary>
    /// Immediately raise all events accumulated for entity.
    /// </summary>
    /// <param name="entityIdentifier">Entity identifier.</param>
    void RaiseEvents(IEntityIdentifier entityIdentifier);

    /// <summary>
    /// Immediately raise all events accumulated for entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    void RaiseEvents(IEntity entity);
  }
}
