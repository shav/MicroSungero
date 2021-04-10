using System.Collections.Generic;

namespace MicroSungero.Kernel.Data
{
  /// <summary>
  /// Provider that gives access to unit-of-work context.
  /// </summary>
  public interface IUnitOfWorkContext
  {
    /// <summary>
    /// Active unit of work.
    /// </summary>
    IUnitOfWork CurrentUnitOfWork { get; }

    /// <summary>
    /// Units-of-work stack (from outer to inner units).
    /// </summary>
    IEnumerable<IUnitOfWork> UnitsOfWorkStack { get; }

    /// <summary>
    /// Factory that creates new unit-of-work instance.
    /// </summary>
    IUnitOfWorkFactory Factory { get; }
  }
}