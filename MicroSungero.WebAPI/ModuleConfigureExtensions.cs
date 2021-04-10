using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MicroSungero.WebAPI
{
  /// <summary>
  /// Extension methods for module configuration.
  /// </summary>
  public static class ModuleConfigureExtensions
  {
    /// <summary>
    /// Basic module configuration.
    /// </summary>
    /// <param name="services">Dependency container.</param>
    /// <param name="modules">Modules for configuration.</param>
    public static void ConfigureModules(this IServiceCollection services, params Assembly[] modules)
    {
      foreach (var module in modules)
      {
        services.AddAutoMapper(module);
        services.AddValidatorsFromAssembly(module);
        services.AddMediatR(module);
      }
    }
  }
}
