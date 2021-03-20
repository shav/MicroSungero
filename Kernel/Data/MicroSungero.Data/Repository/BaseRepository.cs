using System;
using System.Linq;
using System.Linq.Expressions;

namespace MicroSungero.Data
{
  /// <summary>
  /// Base repository implementation.
  /// </summary>
  /// <typeparam name="TRecord">Type of records at storage.</typeparam>
  public abstract class BaseRepository<TRecord> : IRepository<TRecord>
    where TRecord : class
  {
    #region IRepository

    public TRecord Get(Expression<Func<TRecord, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IQueryable<TRecord> GetAll()
    {
      throw new NotImplementedException();
    }

    public IQueryable<TRecord> GetAll(Expression<Func<TRecord, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public TRecord Create()
    {
      throw new NotImplementedException();
    }

    public void Update(TRecord record)
    {
      throw new NotImplementedException();
    }

    public void Delete(TRecord record)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
