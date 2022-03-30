namespace MeetupPlatformApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetSectionValueFromJwt(this IConfiguration configuration, string sectionName)
        {
            return configuration.GetSection("Jwt").GetSection(sectionName).Value;
        }
    }
}
