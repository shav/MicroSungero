using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MicroSungero.WebAPI.Configuration
{
  /// <summary>
  /// Extension methods for swagger configuration on service.
  /// </summary>
  public static class SwaggerConfigureExtensions
  {
    /// <summary>
    /// Add swagger page to service.
    /// </summary>
    /// <param name="app">Application configurator.</param>
    /// <param name="serviceName">Service name.</param>
    /// <returns>Application with configured swagger.</returns>
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, string serviceName)
    {
      app.UseSwagger(c =>
      {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        {
          swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/{serviceName}" } };
        });
      });
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("swagger/v1/swagger.json", $"{serviceName} Service API");
        c.RoutePrefix = "";
      });
      app.UseStaticFiles("/swagger");

      return app;
    }

    /// <summary>
    /// Enable swagger documentation for service.
    /// </summary>
    /// <param name="services">Dependency container.</param>
    /// <param name="serviceName">Service name.</param>
    public static void UseSwaggerGenerator(this IServiceCollection services, string serviceName)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{serviceName} Service API", Version = "v1" });
      });
    }
  }
}
