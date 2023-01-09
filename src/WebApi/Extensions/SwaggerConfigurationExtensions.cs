using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebApi.Swagger;

namespace WebApi.Extensions;

public static class SwaggerConfigurationExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        //Add services and configuration to use swagger
        services.AddSwaggerGen(options =>
        {
            var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
            //show controller XML comments like summary
            options.IncludeXmlComments(xmlDocPath, true);

            options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "API V1" });

            #region Versioning
            // Remove version parameter from all Operations
            options.OperationFilter<RemoveVersionParameters>();

            //set version "api/v{version}/[controller]" from current swagger doc verion
            options.DocumentFilter<SetVersionInPaths>();

            //Seperate and categorize end-points by doc version
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => docName.Equals($"v{v}", StringComparison.OrdinalIgnoreCase));
            });
            #endregion
        });
    }

    public static IApplicationBuilder UseSwaggerAndSwaggerUI(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));
        
        app.UseSwagger();

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");

            options.DocExpansion(DocExpansion.None);
        });

        return app;
    }
}