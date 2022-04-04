namespace MeetupPlatformApi.Features.Authentication.AuthenticateUser;

using System.ComponentModel.DataAnnotations;

public class TokenDto
{
    /// <summary>
    /// Access token JWT string
    /// </summary>
    /// <example>*JWT string*</example>
    [Required]
    public string AccessToken { get; set; }
}
