namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Validator that checks validity of entity before saving it.
  /// </summary>
  /// <typeparam name="TEntity">Type of saving entity.</typeparam>
  public interface ISaveEntityValidator<TEntity> : IEntityValidator<TEntity>
  {
  }
}
