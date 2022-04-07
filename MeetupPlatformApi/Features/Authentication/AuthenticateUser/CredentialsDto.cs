namespace MeetupPlatformApi.Features.Authentication.AuthenticateUser;

using System.ComponentModel.DataAnnotations;

public class CredentialsDto
{
    /// <summary>
    /// User login name
    /// </summary>
    /// <example>Inan1965</example>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// User password
    /// </summary>
    /// <example>Passw0rd123</example>
    [Required]
    public string Password { get; set; }
}
