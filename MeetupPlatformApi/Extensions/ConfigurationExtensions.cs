namespace MeetupPlatformApi.Extensions;

public static class ConfigurationExtensions
{
    public static string GetSectionValueFromJwt(this IConfiguration configuration, string sectionName)
    {
        return configuration[$"Jwt:{sectionName}"];
    }
}
