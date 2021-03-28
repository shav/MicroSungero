namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Validator that checks possibility of deleting entity.
  /// </summary>
  /// <typeparam name="TEntity">Type of deleting entity.</typeparam>
  public interface IDeleteEntityValidator<TEntity> : IEntityValidator<TEntity>
  {
  }
}
