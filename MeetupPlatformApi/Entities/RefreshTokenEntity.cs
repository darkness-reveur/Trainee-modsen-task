namespace MeetupPlatformApi.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RefreshTokenEntity
{
    [Required]
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime Expires { get; set; }

    [ForeignKey(nameof(UserEntity))]
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public UserEntity User { get; set; }
}
