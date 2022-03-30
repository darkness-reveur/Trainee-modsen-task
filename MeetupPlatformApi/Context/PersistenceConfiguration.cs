namespace MeetupPlatformApi.Context;

using MeetupPlatformApi.Configuration;

public class PersistenceConfiguration
{
    public string ConnectionString { get; }

    public PersistenceConfiguration(IConfiguration applicationConfiguration)
    {
        const string persistenceSectionName = "Persistence";
        var host = applicationConfiguration.GetRequiredOption($"{persistenceSectionName}:Host");
        var port = applicationConfiguration.GetRequiredOption($"{persistenceSectionName}:Port");
        var database = applicationConfiguration.GetRequiredOption($"{persistenceSectionName}:Database");
        var username = applicationConfiguration.GetRequiredOption($"{persistenceSectionName}:Username");
        var password = applicationConfiguration.GetRequiredOption($"{persistenceSectionName}:Password");
        
        ConnectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
    }
}