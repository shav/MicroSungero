using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Data
{
  /// <summary>
  /// Implementation of "unit of work" pattern.
  /// The scope that maintains and tracks changes of a list of objects affected by a business transaction.
  /// </summary>
  public class Session : IUnitOfWork, IDisposable
  {
    #region Properties and fields

    /// <summary>
    /// Current active session.
    /// </summary>
    /// <remarks>Returns the most inner session within session stack.</remarks>
    public static Session Current => SessionStack.LastOrDefault();

    private static readonly AsyncLocal<ICollection<Session>> sessionStack = new AsyncLocal<ICollection<Session>>();

    /// <summary>
    /// Session stack.
    /// </summary>
    /// <remarks>
    /// Session can be implicitly wrapped by other outer session 
    /// (beacuse when creating new session you don't know and shouldn't care about there is outer session or not).
    /// All wrapped sessions stack is treated as single unit of work.
    /// It means that all changes from inner sessions actually will be submitted only on submitting changes of the most outer session as single transaction,
    /// but not when calling the method <see cref="SubmitChanges"/> of the inner session.
    /// </remarks>
    private static ICollection<Session> SessionStack
    {
      get 
      { 
        return sessionStack.Value ?? (sessionStack.Value = new Collection<Session>());
      }
      set 
      { 
        sessionStack.Value = value;
      }
    }

    #endregion

    #region IUnitOfWork

    public TRecord Create<TRecord>() where TRecord : class
    {
      throw new NotImplementedException();
    }

    public TRecord Attach<TRecord>(TRecord record) where TRecord : class
    {
      throw new NotImplementedException();
    }

    public void Delete<TRecord>(TRecord record) where TRecord : class
    {
      throw new NotImplementedException();
    }

    public IQueryable<TRecord> GetAll<TRecord>() where TRecord : class
    {
      throw new NotImplementedException();
    }

    public TEntity GetById<TEntity>(int id) where TEntity : class, IEntity
    {
      throw new NotImplementedException();
    }

    public Task SubmitChanges()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region Constructors

    public Session()
    {
      SessionStack.Add(this);
    }

    #endregion

    #region IDisposable

    private bool disposed = false;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;

      if (disposing)
      {
        SessionStack.Remove(this);
      }
      this.disposed = true;
    }

    ~Session()
    {
      this.Dispose(false);
    }

    #endregion
  }
}
