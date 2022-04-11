﻿namespace MeetupPlatformApi.Swagger;

using MeetupPlatformApi.Seedwork.Configuration;

public class SwaggerConfiguration
{
    public bool EnableSwagger { get; }
    
    public bool HideSchemasSection { get; }

    public SwaggerConfiguration(IConfiguration applicationConfiguration)
    {
        var swaggerConfiguration = applicationConfiguration.FromSection("Swagger");

        EnableSwagger = swaggerConfiguration.GetRequiredBool("EnableSwagger");
        HideSchemasSection = swaggerConfiguration.GetRequiredBool("HideSchemasSection");
    }
}
