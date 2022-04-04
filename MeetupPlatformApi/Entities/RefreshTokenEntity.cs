namespace MeetupPlatformApi.Entities;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }

    public DateTime Expires { get; set; }

    public Guid UserId { get; set; }
    
    public UserEntity User { get; set; }
}
