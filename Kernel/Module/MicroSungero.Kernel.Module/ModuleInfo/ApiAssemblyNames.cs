namespace MicroSungero.ModuleInfo
{
  /// <summary>
  /// API assemblies names info.
  /// </summary>
  public class ApiAssemblyNames
  {
    /// <summary>
    /// Behaviors assembly name.
    /// </summary>
    public string Behaviors => $"{this}.{nameof(Behaviors)}";

    /// <summary>
    /// Services assembly name.
    /// </summary>
    public string Services => $"{this}.{nameof(Services)}";

    /// <summary>
    /// Get default API assembly name.
    /// </summary>
    /// <param name="obj">API assemblies info.</param>
    public static implicit operator string(ApiAssemblyNames obj)
    {
      return $"{obj.moduleName}.{nameof(ModuleAssemblyNames.API)}";
    }

    /// <summary>
    /// Module name.
    /// </summary>
    private readonly string moduleName;

    /// <summary>
    /// Create API assemblies names info.
    /// </summary>
    /// <param name="moduleName">Module name.</param>
    public ApiAssemblyNames(string moduleName)
    {
      this.moduleName = moduleName;
    }
  }
}
