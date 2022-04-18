namespace MeetupPlatform.Api.Seedwork.Configuration;

public class InvalidConfigurationException : Exception
{
    public InvalidConfigurationException(string message)
        : base(message)
    {
    }
}
