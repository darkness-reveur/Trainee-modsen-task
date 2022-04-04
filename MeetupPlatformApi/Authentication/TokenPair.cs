namespace MeetupPlatformApi.Authentication;

using MeetupPlatformApi.Entities;

public class TokenPair
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpires { get; set; }
}
