namespace MeetupPlatformApi.Seedwork.Configuration;

public static class ConfigurationExtensions
{
    public static void AddJsonFiles(this IConfigurationBuilder configuration, IHostEnvironment environment)
    {
        var jsonFileNames = new[]
        {
            "appSettings.json",
            $"appSettings.{environment.EnvironmentName}.json"
        };

        foreach (var jsonFileName in jsonFileNames)
        {
            var jsonFilePath = Path.Combine("Properties", jsonFileName);
            configuration.AddJsonFile(jsonFilePath, optional: true);
        }
    }

    public static ApplicationConfigurationSection FromSection(this IConfiguration configuration, string sectionName) =>
        new(configuration, sectionName);
}
