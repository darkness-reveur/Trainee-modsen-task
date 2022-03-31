namespace MeetupPlatformApi.Context;

using MeetupPlatformApi.Configuration;

public class PersistenceConfiguration
{
    public string ConnectionString { get; }

    public PersistenceConfiguration(IConfiguration applicationConfiguration)
    {
        const string persistenceSectionName = "Persistence";
        var host = applicationConfiguration.GetString($"{persistenceSectionName}:Host");
        var port = applicationConfiguration.GetString($"{persistenceSectionName}:Port");
        var database = applicationConfiguration.GetString($"{persistenceSectionName}:Database");
        var username = applicationConfiguration.GetString($"{persistenceSectionName}:Username");
        var password = applicationConfiguration.GetString($"{persistenceSectionName}:Password");
        
        ConnectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
    }
}
