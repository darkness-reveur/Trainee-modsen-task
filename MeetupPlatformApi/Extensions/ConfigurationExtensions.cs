namespace MeetupPlatformApi.Extensions;

public static class ConfigurationExtensions
{
    public static string GetSectionValueFromJwt(this IConfiguration configuration, string sectionName)
    {
        return configuration[$"Jwt:{sectionName}"];
    }

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
}
