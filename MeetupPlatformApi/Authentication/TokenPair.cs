namespace MeetupPlatformApi.Authentication;

using MeetupPlatformApi.Entities;

public class TokenPair
{
    public string AccessToken { get; set; }

    public RefreshTokenEntity RefreshToken { get; set; }
}
