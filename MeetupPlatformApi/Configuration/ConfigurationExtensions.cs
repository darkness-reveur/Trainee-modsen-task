namespace MeetupPlatformApi.Configuration;

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

    public static string GetString(this IConfiguration configuration, string path)
    {
        var value = configuration[path];
        return string.IsNullOrWhiteSpace(value)
            ? throw new InvalidConfigurationException($"Configuration parameter \"{path}\" is required.")
            : value.Trim();
    }
    
    public static int GetInt(this IConfiguration configuration, string path)
    {
        var stringValue = configuration.GetString(path);
        if (!int.TryParse(stringValue, out var value))
        {
            throw new InvalidConfigurationException("Configuration parameter \"{path}\" must be an integer number.");
        }
        return value;
    }
}
