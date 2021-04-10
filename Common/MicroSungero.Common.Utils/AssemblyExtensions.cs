using System;
using System.Linq;
using System.Reflection;

namespace MicroSungero.Common.Utils
{
  /// <summary>
  /// Extension methods for assemblies.
  /// </summary>
  public static class AssemblyExtensions
  {
    /// <summary>
    /// Get assembly loaded in the application domain by full or partial name.
    /// </summary>
    /// <param name="domain">Application domain.</param>
    /// <param name="assemblyName">Full or partial assembly name.</param>
    /// <returns>Assembly.</returns>
    public static Assembly GetAssemblyByName(this AppDomain domain, string assemblyName)
    {
      var assembly = domain.GetAssemblies()
        .FirstOrDefault(a => a.GetName().Name == assemblyName || a.GetName().FullName == assemblyName);

      try
      {
        if (assembly == null)
          assembly = Assembly.Load(assemblyName);
      }
      catch
      {
        // TODO: Log error
        throw;
      }

      try
      {
        if (assembly == null)
          assembly = Assembly.LoadWithPartialName(assemblyName);
      }
      catch
      {
        // TODO: Log error
        throw;
      }

      return assembly;
    }
  }
}
