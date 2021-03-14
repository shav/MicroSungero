namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// The root object of domain aggregate.
  /// Aggregate is a cluster of related domain objects that can be treated as a single unit.
  /// </summary>
  /// <remarks>
  /// Aggregates draw a boundary around one or more Entities.
  /// An Aggregate enforces invariants for all its Entities for any operation it supports.
  /// Each Aggregate has a Root Entity, which is the only member of the Aggregate that any object outside the Aggregate is allowed to hold a reference to.
  /// 
  /// See more for details:
  /// https://martinfowler.com/bliki/DDD_Aggregate.html
  /// https://lostechies.com/jimmybogard/2008/05/21/entities-value-objects-aggregates-and-roots/
  /// </remarks>
  public interface IAggregateRoot
  {
  }
}
