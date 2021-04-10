namespace MicroSungero.ModuleInfo
{
  /// <summary>
  /// Domain assemblies names info.
  /// </summary>
  public class DomainAssemblyNames
  {
    #region Properties and fields

    /// <summary>
    /// Abstractions assembly name.
    /// </summary>
    public string Abstractions => $"{this}.{nameof(Abstractions)}";

    /// <summary>
    /// Services assembly name.
    /// </summary>
    public string Services => $"{this}.{nameof(Services)}";

    /// <summary>
    /// Entities assembly name.
    /// </summary>
    public string Entities => $"{this}.{nameof(Entities)}";

    /// <summary>
    /// Exceptions assembly name.
    /// </summary>
    public string Exceptions => $"{this}.{nameof(Exceptions)}";

    /// <summary>
    /// Get default domain assembly name.
    /// </summary>
    /// <param name="obj">Domain assemblies info.</param>
    public static implicit operator string(DomainAssemblyNames obj)
    {
      return $"{obj.moduleName}.{nameof(ModuleAssemblyNames.Domain)}";
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
    /// Create domain assemblies names info.
    /// </summary>
    /// <param name="moduleName">Module name.</param>
    public DomainAssemblyNames(string moduleName)
    {
      this.moduleName = moduleName;
    }

    #endregion
  }
}
