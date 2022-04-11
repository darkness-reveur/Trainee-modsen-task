namespace MeetupPlatformApi.Swagger;

using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class ConfigurationExtensions
{
    public static void AddSwashbuckleSwagger(this IServiceCollection services, IConfiguration applicationConfiguration)
    {
        var swaggerConfiguration = new SwaggerConfiguration(applicationConfiguration);
        services.AddSingleton(swaggerConfiguration);

        if (!swaggerConfiguration.EnableSwagger)
        {
            return;
        }
        
        services.AddSwaggerGen(options =>
        {
            options.ConfigureOpenApiInfo();
            options.EnrichSchemaDocumentation();
            options.ConfigureAuthentication();
        });
    }

    public static void UseSwashbuckleSwagger(this IApplicationBuilder application)
    {
        var swaggerConfiguration = application.ApplicationServices.GetRequiredService<SwaggerConfiguration>();

        if (!swaggerConfiguration.EnableSwagger)
        {
            return;
        }
        
        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            if (swaggerConfiguration.HideSchemasSection)
            {
                options.DefaultModelsExpandDepth(-1);
            }
        });
    }

    private static void ConfigureOpenApiInfo(this SwaggerGenOptions options)
    {
        const string apiVersion = "v1";
        
        options.SwaggerDoc(apiVersion, new OpenApiInfo
        {
            Version = apiVersion,
            Title = "MeetupApi API",
            Description = "An ASP.NET Core Web API for managing meetup items"
        });
    }

    private static void EnrichSchemaDocumentation(this SwaggerGenOptions options)
    {
        options.CustomSchemaIds(modelType => modelType.FullName?.Replace("+", "."));

        var xmlCommentsFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFileName);
        options.IncludeXmlComments(xmlCommentsFilePath);
    }

    private static void ConfigureAuthentication(this SwaggerGenOptions options)
    {
        const string schemaName = "Bearer";
        
        options.AddSecurityDefinition(schemaName, new OpenApiSecurityScheme
        {
            Description = "Put Your Access Token here:",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>(true, schemaName);
        options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
    }
}
