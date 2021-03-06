namespace MicroSungero.ModuleInfo
{
  /// <summary>
  /// Data assemblies names info.
  /// </summary>
  public class DataAssemblyNames
  {
    #region Properties and fields

    /// <summary>
    /// Abstractions assembly name.
    /// </summary>
    public string Abstractions => $"{this}.{nameof(Abstractions)}";

    /// <summary>
    /// EntityFramework implementation assembly name.
    /// </summary>
    public string EntityFramework => $"{this}.{nameof(EntityFramework)}";

    /// <summary>
    /// Get default data assembly name.
    /// </summary>
    /// <param name="obj">Data assemblies info.</param>
    public static implicit operator string(DataAssemblyNames obj)
    {
      return $"{obj.moduleName}.{nameof(ModuleAssemblyNames.Data)}";
    }

    /// <summary>
    /// Module name.
    /// </summary>
    private readonly string moduleName;

    #endregion

    #region Object

    public override string ToString()
    {
      return (string)this;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Create data assemblies names info.
    /// </summary>
    /// <param name="moduleName">Module name.</param>
    public DataAssemblyNames(string moduleName)
    {
      this.moduleName = moduleName;
    }

    #endregion
  }
}
