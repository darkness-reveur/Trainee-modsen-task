namespace MeetupPlatformApi.Persistence.SwaggerDocConfiguration;

using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerSwashbuckleConfiguration
{
    public static void AddSwaggerSwashbuckleConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerSwashbuckleOptionsConfiguration>();

        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(modelType => modelType.FullName);

            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        });
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
    }
}

