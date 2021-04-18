using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicroSungero.Kernel.Data;
using MicroSungero.Kernel.Data.EntityFramework;
using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.WebAPI.Configuration
{
  /// <summary>
  /// Database configure extensions.
  /// </summary>
  public static class DatabaseConfigureExtensions
  {
    /// <summary>
    /// Configure database services.
    /// </summary>
    /// <typeparam name="TDbContext">Type of database context.</typeparam>
    /// <param name="services">Dependency container.</param>
    /// <param name="configuration">App configuration.</param>
    public static void ConfigureDatabase<TDbContext, TDbContextFactory>(this IServiceCollection services, IConfiguration configuration)
      where TDbContext : BaseDbContext
      where TDbContextFactory: class, IDbContextFactory
    {
      services.AddTransient<IDbContextFactory, TDbContextFactory>(provider =>
      {
        var databaseSettings = configuration.GetAppSettings()?.DatabaseSettings;
        if (databaseSettings == null)
          throw new InvalidOperationException("Database settings are not defined at config.");

        var connectionSettings = new DatabaseConnectionSettings
        {
          ConnectionString = databaseSettings.ConnectionString,
          TransactionIsolationLevel = databaseSettings.TransactionIsolationLevel,
          ServerType = databaseSettings.ServerType
        };

        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
        switch(databaseSettings.ServerType)
        {
          case DatabaseServerType.MSSQLServer:
            optionsBuilder.UseSqlServer(databaseSettings.ConnectionString);
            break;
          case DatabaseServerType.PostgreSQL:
            optionsBuilder.UseNpgsql(databaseSettings.ConnectionString);
            break;

        }
        return (TDbContextFactory)Activator.CreateInstance(typeof(TDbContextFactory), optionsBuilder.Options, connectionSettings);
      });
    }

    /// <summary>
    /// Configure repository for entity type.
    /// </summary>
    /// <typeparam name="TEntity">Type of entities.</typeparam>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    /// <param name="services">Dependency container.</param>
    public static void ConfigureRepository<TEntity, TRepository>(this IServiceCollection services)
      where TEntity : IEntity
      where TRepository : class, IRepository<TEntity>, IEntityRepository<TEntity>
    {
      services.AddTransient<IRepository<TEntity>, TRepository>();
      services.AddTransient<IEntityRepository<TEntity>, TRepository>();
    }
  }
}
