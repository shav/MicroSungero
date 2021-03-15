using System;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Service that validates entity (attributes, state, etc.) using all registered validators for entity.
  /// </summary>
  /// <typeparam name="TEntity">Type of entity.</typeparam>
  public interface IEntityValidationService<TEntity>
  {
    /// <summary>
    /// Validate entity using all registered validators of specified type of validators.
    /// </summary>
    /// <typeparam name="TValidator">Validators type.</typeparam>
    /// <param name="entity">Entity for validation.</param>
    /// <param name="throwOnErrors">Should throw exception on validation error?</param>
    /// <returns>Validation exception (if it has not been thrown).</returns>
    Exception Validate<TValidator>(TEntity entity, bool throwOnErrors = true) where TValidator : IEntityValidator<TEntity>;
  }

  /// <summary>
  /// Service that validates any type of entities (attributes, state, etc.) using all registered validators for entity.
  /// </summary>
  public interface IEntityValidationService : IEntityValidationService<IEntity>
  {
  }
}
