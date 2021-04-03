using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace MicroSungero.WebAPI
{
  /// <summary>
  /// Extension methods to add swagger page to service.
  /// </summary>
  public static class SwaggerAppBuilderExtensions
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
  }
}
