namespace MeetupPlatformApi.Seedwork.Configuration;

public static class ConfigurationExtensions
{
    /// <summary>Overrides default configuration sources.</summary>
    /// <remarks>
    /// Loads configuration from the following sources (in this specific order):
    /// <list type="number">
    /// <item>Default ASP's In-Memory provider.</item>
    /// <item>Default ASP's Chained provider.</item>
    /// <item>"Properties/appSettings.json" file.</item>
    /// <item>Environment variables.</item>
    /// <item>"Properties/appSettings.{Environment}.json" file.</item>
    /// </list>
    /// </remarks>
    public static void OverrideConfigurationSources(this WebApplicationBuilder webApplication)
    {
        IConfigurationBuilder configuration = webApplication.Configuration;
        var environment = webApplication.Environment;
        
        configuration.Sources.Strip();
        configuration.AddBaseAppSettingsJson();
        configuration.AddEnvironmentVariables();
        configuration.AddEnvironmentSpecificAppSettingsJson(environment);
    }

    /// <summary>Removes all unnecessary configuration sources from provided collection.</summary>
    private static void Strip(this ICollection<IConfigurationSource> configurationSources)
    {
        // Only the 2 first sources are crucial to the ASP application
        var redundantSources = configurationSources.Skip(2).ToList();
        redundantSources.ForEach(redundantSource => configurationSources.Remove(redundantSource));
    }

    /// <summary>Add the base "Properties/appSettings.json" file.</summary>
    private static void AddBaseAppSettingsJson(this IConfigurationBuilder configuration)
    {
        var jsonFilePath = Path.Combine("Properties", "appSettings.json");
        configuration.AddJsonFile(jsonFilePath, optional: true);
    }

    /// <summary>Add the environment-specific "Properties/appSettings.{Environment}.json" file.</summary>
    private static void AddEnvironmentSpecificAppSettingsJson(this IConfigurationBuilder configuration, IHostEnvironment environment)
    {
        var jsonFileName = $"appSettings.{environment.EnvironmentName}.json";
        var jsonFilePath = Path.Combine("Properties", jsonFileName);
        configuration.AddJsonFile(jsonFilePath, optional: true);
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
