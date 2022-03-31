using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.Entities;

public class UserEntity
{
    [Required]
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
}
