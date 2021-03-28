using MicroSungero.Kernel.Domain;

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
      if (UnitOfWorkScope.Current != null)
        this.Create(UnitOfWorkScope.Current.DbContext);

      return new UnitOfWork(this.dbContextFactory, this.entityLifetimeService);
    }

    public IUnitOfWork Create(IDbContext dbContext)
    {
      return new UnitOfWork(dbContext, this.entityLifetimeService);
    }

    #endregion

    #region Properties and fields 

    /// <summary>
    /// Database context factory.
    /// </summary>
    private IDbContextFactory dbContextFactory;

    /// <summary>
    /// Service that manages entity lifetime.
    /// </summary>
    private IEntityLifetimeService entityLifetimeService;

    #endregion

    #region Constructors

    /// <summary>
    /// Create unit-of-work factory.
    /// </summary>
    /// <param name="dbContextFactory">Database context factory.</param>
    /// <param name="entityLifetimeService">Service that manages entity lifetime.</param>
    public UnitOfWorkFactory(IDbContextFactory dbContextFactory, IEntityLifetimeService entityLifetimeService)
    {
      this.dbContextFactory = dbContextFactory;
      this.entityLifetimeService = entityLifetimeService;
    }

    #endregion
  }
}
