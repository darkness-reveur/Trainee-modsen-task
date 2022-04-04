namespace MeetupPlatformApi.Features.Authentication.AuthenticateUser;

using System.ComponentModel.DataAnnotations;

public class TokenDto
{
    [Required]
    public string AccessToken { get; set; }
}
