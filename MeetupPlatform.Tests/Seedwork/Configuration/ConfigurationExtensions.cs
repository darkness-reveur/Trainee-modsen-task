namespace MeetupPlatform.Tests.Seedwork.Configuration;

using System.IO;
using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    /// <summary>Add the base "Properties/appSettings.json" file.</summary>
    public static IConfigurationBuilder AddBaseAppSettingsJson(this IConfigurationBuilder configuration)
    {
        var jsonFilePath = Path.Combine("Properties", "appSettings.json");
        return configuration.AddJsonFile(jsonFilePath, optional: false);
    }
    
    /// <summary>Add the "Properties/appSettings.Local.json" file.</summary>
    public static IConfigurationBuilder AddLocalAppSettingsJson(this IConfigurationBuilder configuration)
    {
        var jsonFilePath = Path.Combine("Properties", "appSettings.Local.json");
        return configuration.AddJsonFile(jsonFilePath, optional: true);
    }
}
