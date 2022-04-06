namespace MeetupPlatformApi.Features.Authentication.AuthenticateUser;

using System.ComponentModel.DataAnnotations;

public class TokenPairDto
{
    /// <summary>
    /// Access token JWT string
    /// </summary>
    /// <example>*JWT string*</example>
    [Required]
    public string AccessToken { get; set; }

    /// <summary>
    /// Refresh token JWT string
    /// </summary>
    /// <example>*JWT string*</example>
    [Required]
    public string RefreshToken { get; set; }
}
