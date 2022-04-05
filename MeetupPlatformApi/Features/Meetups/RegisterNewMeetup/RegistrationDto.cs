namespace MeetupPlatformApi.Features.Meetups.RegisterNewMeetup;

using System.ComponentModel.DataAnnotations;

public class RegistrationDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    [MaxLength(4000)]
    public string Description { get; set; }
}
