namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using System.ComponentModel.DataAnnotations;

public class RegistrationDto
{
    /// <summary>
    /// User login name
    /// </summary>
    /// <example>Inan1965</example>
    [Required]
    [MaxLength(45)]
    public string Username { get; set; }

    /// <summary>
    /// User password
    /// </summary>
    /// <example>Passw0rd123</example>
    [Required]
    [MaxLength(30)]
    public string Password { get; set; }
}
