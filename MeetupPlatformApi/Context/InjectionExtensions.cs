namespace MeetupPlatformApi.Context;

using Microsoft.EntityFrameworkCore;

public static class InjectionExtensions
{
    public static void AddDbContext(this IServiceCollection services) =>
        services.AddDbContext<ApplicationContext>((serviceProvider, contextOptions) =>
        {
            var applicationConfiguration = serviceProvider.GetRequiredService<IConfiguration>();
            var persistenceConfiguration = new PersistenceConfiguration(applicationConfiguration);

            contextOptions.UseNpgsql(persistenceConfiguration.ConnectionString);
        });
}
