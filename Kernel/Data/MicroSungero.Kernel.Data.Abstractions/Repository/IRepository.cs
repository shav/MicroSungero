using System;
using System.Linq;
using System.Linq.Expressions;

namespace MicroSungero.Data
{
  /// <summary>
  /// Repository is used for accessing to data in storage (in database, memory, etc.) like a collection of objects.
  /// Supports CRUD operations.
  /// </summary>
  /// <typeparam name="TRecord">Type of data objects or records.</typeparam>
  /// <remarks>
  /// A repository performs the tasks of an intermediary between the domain model layers and data mapping,
  /// acting in a similar way to a set of domain objects in memory.
  /// Client objects declaratively build queries and send them to the repositories for answers.
  /// Conceptually, a repository encapsulates a set of objects stored in the database and operations that can be performed on them,
  /// providing a way that is closer to the persistence layer.
  /// Repositories, also, support the purpose of separating, clearly and in one direction,
  /// the dependency between the work domain and the data allocation or mapping.
  /// See more for details:
  /// https://martinfowler.com/eaaCatalog/repository.html
  /// https://rusyasoft.github.io/ddd/2018/05/10/ddd-repository/
  /// https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
  /// </remarks>
  public interface IRepository<TRecord>
  {
    /// <summary>
    /// Get all records as unclosed query.
    /// </summary>
    /// <returns>All records query.</returns>
    IQueryable<TRecord> GetAll();

    /// <summary>
    /// Get all records satisfying passed filter as unclosed query.
    /// </summary>
    /// <param name="filter">Criteria for filtering records.</param>
    /// <returns>Query of records satisfying passed filter.</returns>
    IQueryable<TRecord> GetAll(Expression<Func<TRecord, bool>> filter);

    /// <summary>
    /// Find single record satisfying passed filter.
    /// If there are some records satisfying passed filter then exception will be thrown.
    /// </summary>
    /// <param name="filter">Criteria for finding record.</param>
    /// <returns></returns>
    TRecord Get(Expression<Func<TRecord, bool>> filter);

    /// <summary>
    /// Create new record of particular type.
    /// </summary>
    /// <returns>New unsaved record.</returns>
    TRecord Create();

    /// <summary>
    /// Update record at the storage.
    /// If record is new and doesn't exist at storage then record will be added to storage.
    /// </summary>
    /// <param name="record">Updating record.</param>
    void Update(TRecord record);

    /// <summary>
    /// Delete record from the storage.
    /// </summary>
    /// <param name="record">Deleting record.</param>
    void Delete(TRecord record);
  }
}
