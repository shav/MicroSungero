namespace MicroSungero.Data
{
  /// <summary>
  /// Factory that creates new unit-of-work instance.
  /// </summary>
  public class UnitOfWorkFactory : IUnitOfWorkFactory
  {
    #region IUnitOfWorkFactory

    public IUnitOfWork Create()
    {
      return new UnitOfWork(this.dbContextFactory);
    }

    #endregion

    /// <summary>
    /// Database context factory.
    /// </summary>
    private IDbContextFactory dbContextFactory;

    /// <summary>
    /// Create unit-of-work factory.
    /// </summary>
    /// <param name="dbContextFactory">Database context factory.</param>
    public UnitOfWorkFactory(IDbContextFactory dbContextFactory)
    {
      this.dbContextFactory = dbContextFactory;
    }
  }
}
