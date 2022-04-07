namespace MeetupPlatformApi.Features.Authentication.ChangeUserCredentials;

using System.ComponentModel.DataAnnotations;

public class UserCredentialsChangeDto
{
    /// <summary>
    /// User login name
    /// </summary>
    /// <example>Inan1965</example>
    [MaxLength(45)]
    public string Username { get; set; }

    /// <summary>
    /// User old password
    /// </summary>
    /// <example>Passw0rd123</example>
    [Required]
    public string OldPassword { get; set; }

    /// <summary>
    /// User new password
    /// </summary>
    /// <example>NewPassw0rd123</example>
    [Required]
    [MaxLength(30)]
    public string NewPassword { get; set; }
}
