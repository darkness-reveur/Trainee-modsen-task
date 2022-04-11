namespace MeetupPlatform.Api.Seedwork.Configuration;

public class ApplicationConfigurationSection
{
    private readonly IConfigurationSection configurationSection;

    public ApplicationConfigurationSection(IConfiguration applicationConfiguration, string sectionName) =>
        configurationSection = applicationConfiguration.GetSection(sectionName);

    public string GetRequiredString(string parameterName) =>
        GetValue(parameterName, required: true);

    public int GetRequiredInt(string parameterName)
    {
        var stringValue = GetValue(parameterName, required: true);
        
        var isValidNumber = int.TryParse(stringValue, out var value);
        if (!isValidNumber)
        {
            var path = GetParameterPath(parameterName);
            throw new InvalidConfigurationException($"Configuration parameter \"{path}\" must be an integer number.");
        }

        return value;
    }

    public bool GetRequiredBool(string parameterName)
    {
        var stringValue = GetValue(parameterName, required: true);

        var isValidBool = bool.TryParse(stringValue, out var value);
        if (!isValidBool)
        {
            var path = GetParameterPath(parameterName);
            throw new InvalidConfigurationException($"Configuration parameter \"{path}\" must be a boolean.");
        }

        return value;
    }
    
    private string GetValue(string parameterName, bool required = false)
    {
        var value = configurationSection.GetValue<string>(parameterName);

        if (required && string.IsNullOrWhiteSpace(value))
        {
            var path = GetParameterPath(parameterName);
            throw new InvalidConfigurationException($"Configuration parameter \"{path}\" is required.");
        }

        return value?.Trim();
    }

    private string GetParameterPath(string parameterName) =>
        $"{configurationSection.Path}:{parameterName}";
}
