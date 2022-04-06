namespace MeetupPlatformApi.Swagger;

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
            options.CustomSchemaIds(modelType => modelType.FullName.Replace("+", "."));

            var xmlCommentsFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFileName);
            options.IncludeXmlComments(xmlCommentsFilePath);

            options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Enter token here.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        });
    }
}