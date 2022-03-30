namespace MeetupPlatformApi.DataTransferObjects;

using System.ComponentModel.DataAnnotations;

public class UserAuthenticationDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
