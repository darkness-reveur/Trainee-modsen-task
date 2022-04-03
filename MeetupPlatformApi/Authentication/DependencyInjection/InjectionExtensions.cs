namespace MeetupPlatformApi.Authentication.DependencyInjection;

using MeetupPlatformApi.Authentication.Configuration;
using MeetupPlatformApi.Authentication.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public static class InjectionExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration applicationConfiguration)
    {
        var authenticationConfiguration = new AuthenticationConfiguration(applicationConfiguration);
        
        services
            .AddSingleton(authenticationConfiguration)
            .AddSingleton<AuthenticationManager>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = authenticationConfiguration.ValidationParameters;
            });
    }
}
