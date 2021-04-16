using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicroSungero.Kernel.Data;
using MicroSungero.Kernel.Data.EntityFramework;
using MicroSungero.Kernel.Domain.Entities;
using MicroSungero.WebAPI.Settings;

namespace MicroSungero.WebAPI
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
        var databaseSettings = configuration.Get<AppSettings>()?.DatabaseSettings;
        var connectionSettings = new DatabaseConnectionSettings
        {
          ConnectionString = databaseSettings.ConnectionString,
          TransactionIsolationLevel = databaseSettings?.TransactionIsolationLevel
        };

        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
        optionsBuilder.UseSqlServer(databaseSettings.ConnectionString);

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
