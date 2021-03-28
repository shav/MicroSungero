namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Validator that checks validity of new created entity.
  /// </summary>
  /// <typeparam name="TEntity">Type of created entity.</typeparam>
  public interface ICreateEntityValidator<TEntity> : IEntityValidator<TEntity>
  {
  }
}
