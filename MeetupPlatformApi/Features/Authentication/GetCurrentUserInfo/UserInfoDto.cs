namespace MeetupPlatformApi.Features.Authentication.GetCurrentUserInfo;

using System.ComponentModel.DataAnnotations;

public class UserInfoDto
{
    /// <summary>
    /// User id value
    /// </summary>
    /// <example>cc3e5bce-a8c0-4258-b663-71ec8f2b6446</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// User login name
    /// </summary>
    /// <example>Inan1965</example>
    [Required]
    public string Username { get; set; }

    [Required]
    public string Role { get; set; }
}
