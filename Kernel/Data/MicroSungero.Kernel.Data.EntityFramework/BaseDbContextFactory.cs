namespace MicroSungero.Data.EntityFramework
{
  /// <summary>
  /// Factory that creates base database context instance.
  /// </summary>
  public class BaseDbContextFactory: IDbContextFactory
  {
    #region IDbContextFactory

    public IDbContext Create()
    {
      throw new System.NotImplementedException();
    }

    #endregion
  }
}
