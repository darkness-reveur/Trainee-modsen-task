namespace MeetupPlatformApi.Features.Meetups.RegisterNewMeetup;

using System.ComponentModel.DataAnnotations;

public class RegisteredMeetupDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public string Description { get; set; }
}
