namespace MeetupPlatformApi.Domain;

public class RefreshToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }
}
