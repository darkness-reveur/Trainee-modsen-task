namespace MeetupPlatformApi.DataTransferObjects;

using System.ComponentModel.DataAnnotations;

public class UserCredentialsChangeDto
{
    [MaxLength(45)]
    public string Username { get; set; }

    [Required]
    public string OldPassword { get; set; }

    [Required]
    [MaxLength(30)]
    public string NewPassword { get; set; }
}
