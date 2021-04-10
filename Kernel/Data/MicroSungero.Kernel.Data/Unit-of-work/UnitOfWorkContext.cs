using System.Collections.Generic;

namespace MicroSungero.Data
{
  /// <summary>
  /// Provider that gives access to unit-of-work context.
  /// </summary>
  public class UnitOfWorkContext : IUnitOfWorkContext
  {
    #region IUnitOfWorkContext

    public IUnitOfWork CurrentUnitOfWork => UnitOfWork.Current;

    public IEnumerable<IUnitOfWork> UnitsOfWorkStack => UnitOfWork.GetUnitsOfWorkStack();

    public IUnitOfWorkFactory Factory => this.unitOfWorkFactory;

    #endregion

    /// <summary>
    /// Unit-of-work factory.
    /// </summary>
    private IUnitOfWorkFactory unitOfWorkFactory;

    /// <summary>
    /// Create unit-of-work context.
    /// </summary>
    /// <param name="unitOfWorkFactory">Unit-of-work factory.</param>
    public UnitOfWorkContext(IUnitOfWorkFactory unitOfWorkFactory)
    {
      this.unitOfWorkFactory = unitOfWorkFactory;
    }
  }
}
