namespace MeetupPlatformApi.DataTransferObjects;

using System.ComponentModel.DataAnnotations;

public class UserOutputDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; }
}
