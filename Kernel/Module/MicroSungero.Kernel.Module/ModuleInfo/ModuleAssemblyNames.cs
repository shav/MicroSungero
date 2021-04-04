namespace MicroSungero.ModuleInfo
{
  /// <summary>
  /// Module assemblies names.
  /// </summary>
  public class ModuleAssemblyNames
  {
    /// <summary>
    /// Domain assemblies names.
    /// </summary>
    public DomainAssemblyNames Domain { get; }

    /// <summary>
    /// API assemblies names.
    /// </summary>
    public ApiAssemblyNames API { get; }

    /// <summary>
    /// Data assemblies names.
    /// </summary>
    public DataAssemblyNames Data { get; }

    /// <summary>
    /// Module name.
    /// </summary>
    private readonly string moduleName;

    /// <summary>
    /// Create module assemblies names.
    /// </summary>
    /// <param name="moduleName">Module name.</param>
    public ModuleAssemblyNames(string moduleName)
    {
      this.moduleName = moduleName;
      this.API = new ApiAssemblyNames(this.moduleName);
      this.Domain = new DomainAssemblyNames(this.moduleName);
      this.Data = new DataAssemblyNames(this.moduleName);
    }
  }
}
