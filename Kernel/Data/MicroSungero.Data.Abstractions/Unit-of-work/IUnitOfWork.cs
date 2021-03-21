using System.Linq;
using System.Threading.Tasks;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Data
{
  /// <summary>
  /// Unit of work is the scope that maintains a list of objects affected by a business transaction 
  /// and coordinates the writing out of changes and the resolution of concurrency problems.
  /// </summary>
  /// <remarks>
  /// See more for details:
  /// https://martinfowler.com/eaaCatalog/unitOfWork.html
  /// https://blog.byndyu.ru/2010/07/2-unit-of-work_10.html
  /// </remarks>
  public interface IUnitOfWork
  {
    /// <summary>
    /// Create new unsaved record.
    /// </summary>
    /// <typeparam name="TRecord">Type of new record.</typeparam>
    /// <returns>New created record.</returns>
    TRecord Create<TRecord>() where TRecord : class;

    /// <summary>
    /// Add record to current scope.
    /// That means start of tracking entity changes (inserts / updates / deletes).
    /// </summary>
    /// <typeparam name="TRecord">Type of record.</typeparam>
    /// <param name="record">Record to add to scope.</param>
    /// <returns></returns>
    TRecord Attach<TRecord>(TRecord record) where TRecord : class;

    /// <summary>
    /// Delete record.
    /// </summary>
    /// <typeparam name="TRecord">Type of deleting record.</typeparam>
    /// <param name="record">Deleting record.</param>
    void Delete<TRecord>(TRecord record) where TRecord : class;

    /// <summary>
    /// Get all records of specified type as query.
    /// </summary>
    /// <typeparam name="TRecord">Type of records.</typeparam>
    /// <returns>Query for all records of specified type.</returns>
    IQueryable<TRecord> GetAll<TRecord>() where TRecord : class;

    /// <summary>
    /// Get single entity by its Id.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="id">Entity id.</param>
    /// <returns>Entity with specified Id.</returns>
    TEntity GetById<TEntity>(int id) where TEntity : class, IEntity;

    /// <summary>
    /// Save changes of tracking records (inserts / updates / deletes) within single transaction.
    /// </summary>
    Task SubmitChanges();
  }
}
