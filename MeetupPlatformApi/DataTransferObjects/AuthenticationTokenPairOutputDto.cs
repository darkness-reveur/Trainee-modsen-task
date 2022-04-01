namespace MeetupPlatformApi.DataTransferObjects;

using System.ComponentModel.DataAnnotations;

public class AuthenticationTokenPairOutputDto
{
    [Required]
    public string AccessToken { get; set; }

    [Required]
    public Guid RefreshTokenId { get; set; }
}
