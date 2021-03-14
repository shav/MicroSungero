using MicroSungero.Kernel.ModuleInfo;

namespace MicroSungero.Kernel
{
  /// <summary>
  /// Kernel module info.
  /// </summary>
  public class Module : ModuleBase
  {
    /// <summary>
    /// Module name.
    /// </summary>
    public const string Name = "Kernel";

    /// <summary>
    /// Module assemblies names.
    /// </summary>
    public static readonly ModuleAssemblyNames AssemblyNames = new ModuleAssemblyNames(Name);
  }
}
