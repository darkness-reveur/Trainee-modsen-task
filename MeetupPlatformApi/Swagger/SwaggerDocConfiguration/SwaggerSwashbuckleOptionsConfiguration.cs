namespace MeetupPlatformApi.Swagger.SwaggerDocConfiguration;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerSwashbuckleOptionsConfiguration : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "MeetupApi API",
            Description = "An ASP.NET Core Web API for managing meetup items"
        });
    }
}

