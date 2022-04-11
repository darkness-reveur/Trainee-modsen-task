namespace MeetupPlatform.Api.Persistence.Configuration;

using MeetupPlatform.Api.Seedwork.Configuration;

public class PersistenceConfiguration
{
    public string ConnectionString { get; }

    public PersistenceConfiguration(IConfiguration applicationConfiguration)
    {
        var persistenceConfiguration = applicationConfiguration.FromSection("Persistence");
        
        var host = persistenceConfiguration.GetRequiredString("Host");
        var port = persistenceConfiguration.GetRequiredString("Port");
        var database = persistenceConfiguration.GetRequiredString("Database");
        var username = persistenceConfiguration.GetRequiredString("Username");
        var password = persistenceConfiguration.GetRequiredString("Password");
        
        ConnectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
    }
}
