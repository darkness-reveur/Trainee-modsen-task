namespace MeetupPlatformApi.Domain.Users;

public abstract class User
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; }

    public abstract string Role { get; }
}
