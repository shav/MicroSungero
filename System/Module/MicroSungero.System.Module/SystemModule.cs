using MicroSungero.ModuleInfo;

namespace MicroSungero.System
{
  /// <summary>
  /// System module info.
  /// </summary>
  public class Module : ModuleBase
  {
    /// <summary>
    /// Module name.
    /// </summary>
    public const string Name = "MicroSungero.System";

    /// <summary>
    /// Module assemblies names.
    /// </summary>
    public static readonly ModuleAssemblyNames AssemblyNames = new ModuleAssemblyNames(Name);
  }
}
