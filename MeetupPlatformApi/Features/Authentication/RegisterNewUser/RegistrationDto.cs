namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using System.ComponentModel.DataAnnotations;

public class RegistrationDto
{
    [Required]
    [MaxLength(45)]
    public string Username { get; set; }

    [Required]
    [MaxLength(30)]
    public string Password { get; set; }
}
