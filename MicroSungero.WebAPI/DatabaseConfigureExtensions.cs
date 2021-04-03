using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicroSungero.Data;
using MicroSungero.Data.EntityFramework;
using MicroSungero.Kernel.Domain.Entities;

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
    /// <param name="services">Service container.</param>
    /// <param name="configuration">App configuration.</param>
    public static void ConfigureDatabase<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : BaseDbContext
    {
      services.AddTransient<IDbContext>(provider =>
      {
        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
      });
    }

    /// <summary>
    /// Configure repository for entity type.
    /// </summary>
    /// <typeparam name="TEntity">Type of entities.</typeparam>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    /// <param name="services">Service container.</param>
    public static void ConfigureRepository<TEntity, TRepository>(this IServiceCollection services)
      where TEntity : IEntity
      where TRepository : class, IRepository<TEntity>, IEntityRepository<TEntity>
    {
      services.AddTransient<IRepository<TEntity>, TRepository>();
      services.AddTransient<IEntityRepository<TEntity>, TRepository>();
    }
  }
}
