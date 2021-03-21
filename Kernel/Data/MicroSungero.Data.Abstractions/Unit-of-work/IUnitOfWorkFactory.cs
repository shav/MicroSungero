namespace MicroSungero.Data
{
  /// <summary>
  /// Factory that creates new unit-of-work instance.
  /// </summary>
  public interface IUnitOfWorkFactory
  {
    /// <summary>
    /// Create new unit-of-work instance.
    /// </summary>
    IUnitOfWork Create();
  }
}
