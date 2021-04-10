using MicroSungero.Kernel.Domain;
using MicroSungero.Kernel.Domain.Entities;

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

      return new UnitOfWork(this.dbContextFactory, this.entityLifetimeService, this.domainEventScope);
    }

    public IUnitOfWork Create(IDbContext dbContext)
    {
      return new UnitOfWork(dbContext, this.entityLifetimeService, this.domainEventScope);
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

    /// <summary>
    /// Transactional domain events scope.
    /// </summary>
    private readonly IEntityDomainEventContext domainEventScope;

    #endregion

    #region Constructors

    /// <summary>
    /// Create unit-of-work factory.
    /// </summary>
    /// <param name="dbContextFactory">Database context factory.</param>
    /// <param name="entityLifetimeService">Service that manages entity lifetime.</param>
    /// <param name="domainEventScope">Transactional domain events scope.</param>
    public UnitOfWorkFactory(IDbContextFactory dbContextFactory, IEntityLifetimeService entityLifetimeService, IEntityDomainEventContext domainEventScope)
    {
      this.dbContextFactory = dbContextFactory;
      this.entityLifetimeService = entityLifetimeService;
      this.domainEventScope = domainEventScope;
    }

    #endregion
  }
}
