using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MicroSungero.Common.Utils;

namespace MicroSungero.WebAPI.Configuration
{
  /// <summary>
  /// Extension methods for System module configuration.
  /// </summary>
  public static class SystemModuleConfigureExtensions
  {
    /// <summary>
    /// Use System module at the application.
    /// </summary>
    /// <param name="services">Dependency container.</param>
    public static void UseSystemModule(this IServiceCollection services)
    {
      var domainModules = new[]
      {
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.Domain),
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.Domain.Abstractions),
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.Domain.Entities),
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.Domain.Services)
      };
      var apiModules = new[]
      {
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.API),
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.API.Services),
      };
      var dataModules = new[]
      {
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.Data),
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.Data.Abstractions),
        AppDomain.CurrentDomain.GetAssemblyByName(System.Module.AssemblyNames.Data.EntityFramework)
      };
      services.ConfigureModules(new[] { domainModules, apiModules, dataModules }.SelectMany(module => module).ToArray());
    }
  }
}
