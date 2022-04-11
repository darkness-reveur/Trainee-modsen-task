namespace MeetupPlatform.Api.Seedwork.Configuration;

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
