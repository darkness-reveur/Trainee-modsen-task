namespace MeetupPlatformApi.Entities;

using System.ComponentModel.DataAnnotations;

public class UserEntity
{
    [Required]
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
