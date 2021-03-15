using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  ///  Service that validates entity (attributes, state, etc.) using all registered validators for entity.
  /// </summary>
  /// <typeparam name="TEntity">Type of entities to validate.</typeparam>
  public class EntityValidationService<TEntity> : IEntityValidationService<TEntity>
  {
    /// <summary>
    /// All registered validators for entities of specified type.
    /// </summary>
    private readonly IEnumerable<IEntityValidator<TEntity>> validators;

    #region IEntityValidationService

    public Exception Validate<TValidator>(TEntity entity, bool throwOnErrors = true) where TValidator : IEntityValidator<TEntity>
    {
      var entityValidators = this.validators.OfType<TValidator>();
      if (entityValidators.Any())
      {
        var validationResults = entityValidators.Select(v => v.Validate(entity, false));
        var errors = validationResults.SelectMany(r => r.Errors).Where(e => e != null).ToList();

        if (errors.Count != 0)
        {
          var exception = new Kernel.Domain.Exceptions.PropertyValidationException(errors);
          if (throwOnErrors)
          {
            throw exception;
          }
          return exception;
        }
      }
      return null;
    }

    #endregion

    /// <summary>
    /// Create entity validation service.
    /// </summary>
    /// <param name="validators">All registered validators for entities of specified type.</param>
    public EntityValidationService(IEnumerable<IValidator<TEntity>> validators)
    {
      this.validators = validators.OfType<IEntityValidator<TEntity>>().ToArray();
    }
  }
}
