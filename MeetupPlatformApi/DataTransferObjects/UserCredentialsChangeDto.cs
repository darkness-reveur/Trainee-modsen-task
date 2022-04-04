namespace MeetupPlatformApi.DataTransferObjects;

using System.ComponentModel.DataAnnotations;

public class UserCredentialsChangeDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string OldPassword { get; set; }

    [Required]
    [MaxLength(30)]
    public string NewPassword { get; set; }
}
