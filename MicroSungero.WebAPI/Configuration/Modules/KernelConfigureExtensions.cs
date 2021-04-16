using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MicroSungero.Common.Utils;
using MicroSungero.Kernel.API;
using MicroSungero.Kernel.API.Behaviors;
using MicroSungero.Kernel.API.Services;
using MicroSungero.Kernel.Data;
using MicroSungero.Kernel.Domain;
using MicroSungero.Kernel.Domain.DomainEvents;
using MicroSungero.Kernel.Domain.Entities;
using MicroSungero.Kernel.Domain.Services;

namespace MicroSungero.WebAPI
{
  /// <summary>
  /// Extension methods for MicroSungero kernel configuration.
  /// </summary>
  public static class KernelConfigureExtensions
  {
    /// <summary>
    /// Use kernel of MicroSungero Framework at the application.
    /// </summary>
    /// <param name="services">Dependency container.</param>
    public static void UseMicroSungeroKernel(this IServiceCollection services)
    {
      services.AddTransient<IQueryService, QueryService>();
      services.AddTransient<ICommandService, CommandService>();
      services.AddTransient<IDomainEventService, DomainEventService>();
      services.AddTransient(typeof(IEntityValidationService<>), typeof(EntityValidationService<>));
      services.AddTransient<IEntityDomainEventContext, EntityDomainEventContext>();
      services.AddTransient<IUnitOfWorkContext, UnitOfWorkContext>();
      services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
      services.AddTransient<IUnitOfWorkScope, UnitOfWorkScope>();
      services.AddTransient<IEntityLifetimeService, EntityLifetimeService>();

      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehaviour<,>));

      var domainModules = new[]
      {
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.Domain),
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.Domain.Abstractions),
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.Domain.Entities),
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.Domain.Services)
      };
      var apiModules = new[]
      {
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.API),
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.API.Services),
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.API.Behaviors)
      };
      var dataModules = new[]
      {
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.Data),
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.Data.Abstractions),
        AppDomain.CurrentDomain.GetAssemblyByName(Kernel.Module.AssemblyNames.Data.EntityFramework)
      };

      services.ConfigureModules(new[] { domainModules, apiModules, dataModules }.SelectMany(module => module).ToArray());
    }
  }
}
