using MicroSungero.Kernel.Domain.Validation;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// A validator that validates entity (attributes, state, etc.)
  /// </summary>
  /// <typeparam name="TEntity">Type of entity.</typeparam>
  public interface IEntityValidator<TEntity>: IErrorValidator<TEntity>
  {
  }
}
