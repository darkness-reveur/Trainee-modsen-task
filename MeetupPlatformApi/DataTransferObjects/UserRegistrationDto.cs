using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects;

public class UserRegistrationDto
{
    [Required]
    [MaxLength(45)]
    public string Username { get; set; }

    [Required]
    [MaxLength(30)]
    public string Password { get; set; }
}
