namespace MeetupPlatformApi.Features.Authentication.GetCurrentUserInfo;

using System.ComponentModel.DataAnnotations;

public class UserInfoDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Role { get; set; }
}
