namespace MeetupPlatformApi.Features.Authentication.AuthenticateUser;

using System.ComponentModel.DataAnnotations;

public class TokenPairDto
{
    [Required]
    public string AccessToken { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}
