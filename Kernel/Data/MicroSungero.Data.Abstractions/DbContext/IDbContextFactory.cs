namespace MicroSungero.Data
{
  /// <summary>
  /// Factory that creates database context implementation.
  /// </summary>
  public interface IDbContextFactory
  {
    /// <summary>
    /// Create database context implementation.
    /// </summary>
    IDbContext Create();
  }
}
