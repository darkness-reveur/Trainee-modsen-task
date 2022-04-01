namespace MeetupPlatformApi.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }

    public DateTime Expires { get; set; }

    public Guid UserId { get; set; }
    
    public UserEntity User { get; set; }
}
