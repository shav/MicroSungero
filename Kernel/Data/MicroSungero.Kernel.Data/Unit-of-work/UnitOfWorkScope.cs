using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MicroSungero.Kernel.Data.Exceptions;

namespace MicroSungero.Kernel.Data
{
  /// <summary>
  /// Scope that wraps all units-of-work created inside the scope.
  /// Provides common database context for whole units-of-work stack wrapped by the scope.
  /// </summary>
  public class UnitOfWorkScope: IUnitOfWorkScope, IDisposable
  {
    #region Properties and fields

    private static AsyncLocal<UnitOfWorkScope> current = new AsyncLocal<UnitOfWorkScope>();

    /// <summary>
    /// Current unit-of work scope that wraps all units-of-work created inside the scope.
    /// </summary>
    public static UnitOfWorkScope Current => current.Value;

    /// <summary>
    /// Common database context that is used by all units-of-work wrapped into this scope.
    /// </summary>
    private IDbContext dbContext;

    /// <summary>
    /// Unit-of-work context.
    /// </summary>
    private IUnitOfWorkContext unitOfWorkContext;

    #endregion

    #region IUnitOfWorkScope

    public IDbContext DbContext => this.dbContext;

    public async Task SubmitChanges()
    {
      this.CheckIfNotDisposed(nameof(SubmitChanges));

      var unitsOfWorkStack = this.unitOfWorkContext.UnitsOfWorkStack.Reverse().ToList();
      if (unitsOfWorkStack.Any())
      {
        foreach (var unitOfWork in unitsOfWorkStack)
          await unitOfWork.SubmitChanges();
      }
      else
      {
        using (var unitOfWork = this.unitOfWorkContext.Factory.Create(this.dbContext))
        {
          await unitOfWork.SubmitChanges();
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Check before executing action if the current unit-of-work scope has not been disposed before.
    /// If this unit-of-work scope is disposed then throws exception.
    /// </summary>
    /// <param name="actionName">[Optional] Action name.</param>
    private void CheckIfNotDisposed(string actionName = default)
    {
      if (this.disposed)
        throw new InvalidOperationException($"Cannot perform action {actionName} because the current {nameof(UnitOfWorkScope)} is disposed.");
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create new unit-of-work scope.
    /// </summary>
    /// <param name="dbContextFactory">Factory that creates database context.</param>
    /// <param name="unitOfWorkContext">Unit-of-work context.</param>
    public UnitOfWorkScope(IDbContextFactory dbContextFactory, IUnitOfWorkContext unitOfWorkContext)
    {
      // TODO: Allow wrapping UnitOfWorkScopes
      if (UnitOfWorkScope.Current != null)
        throw new UnitOfWorkException($"Cannot create new {nameof(UnitOfWorkScope)}: outer {nameof(UnitOfWorkScope)} already exists and wrapping {nameof(UnitOfWorkScope)}s is not allowed");

      if (dbContextFactory == null)
        throw new UnitOfWorkException($"Cannot create new {nameof(IDbContext)}: {nameof(dbContextFactory)} is not assigned");

      if (unitOfWorkContext == null)
        throw new UnitOfWorkException($"Cannot create new {nameof(IDbContext)}: {nameof(unitOfWorkContext)} is not assigned");

      this.dbContext = dbContextFactory.Create();
      this.unitOfWorkContext = unitOfWorkContext;
      UnitOfWorkScope.current.Value = this;
    }

    #endregion

    #region IDisposable

    private bool disposed;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          (this.dbContext as IDisposable)?.Dispose();
          this.dbContext = null;

          if (ReferenceEquals(UnitOfWorkScope.Current, this))
            UnitOfWorkScope.current.Value = null;
        }
        this.disposed = true;
      }
    }

    ~UnitOfWorkScope()
    {
      this.Dispose(false);
    }

    #endregion
  }
}
