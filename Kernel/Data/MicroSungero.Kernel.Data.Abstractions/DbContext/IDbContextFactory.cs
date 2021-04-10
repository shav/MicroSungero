namespace MicroSungero.Kernel.Data
{
  /// <summary>
  /// Factory that creates database context instance.
  /// </summary>
  public interface IDbContextFactory
  {
    /// <summary>
    /// Create database context instance.
    /// </summary>
    IDbContext Create();
  }
}
