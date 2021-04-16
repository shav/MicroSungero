using System;
using System.Threading.Tasks;

namespace MicroSungero.Kernel.Data
{
  /// <summary>
  /// Scope that wraps all units-of-work created inside the scope.
  /// Provides common database context for whole units-of-work stack wrapped by the scope.
  /// </summary>
  public interface IUnitOfWorkScope: IDisposable
  {
    /// <summary>
    /// Common database context that is used by all units-of-work wrapped into this scope.
    /// </summary>
    IDbContext DbContext { get; }

    /// <summary>
    /// Save changes for whole units-of-work stack that is wrapped by this scope.
    /// </summary>
    Task SubmitChanges();
  }
}
