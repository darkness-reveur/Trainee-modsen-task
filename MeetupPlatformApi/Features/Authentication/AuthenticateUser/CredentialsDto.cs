namespace MeetupPlatformApi.Features.Authentication.AuthenticateUser;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Hello Hello Hello
/// </summary>
public class CredentialsDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
