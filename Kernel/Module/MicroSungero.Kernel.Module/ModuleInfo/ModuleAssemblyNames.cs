namespace MicroSungero.Kernel.ModuleInfo
{
  /// <summary>
  /// Module assemblies names info.
  /// </summary>
  public class ModuleAssemblyNames
  {
    /// <summary>
    /// Domain assembly name.
    /// </summary>
    public string Domain => $"{this.moduleName}.{nameof(Domain)}";

    /// <summary>
    /// API assembly name.
    /// </summary>
    public string API => $"{this.moduleName}.{nameof(API)}";

    /// <summary>
    /// Infrastructure assembly name.
    /// </summary>
    public string Infrastructure => $"{this.moduleName}.{nameof(Infrastructure)}";

    /// <summary>
    /// Module name.
    /// </summary>
    private readonly string moduleName;

    /// <summary>
    /// Create module assemblies names info.
    /// </summary>
    /// <param name="moduleName">Module name.</param>
    public ModuleAssemblyNames(string moduleName)
    {
      this.moduleName = moduleName;
    }
  }
}
