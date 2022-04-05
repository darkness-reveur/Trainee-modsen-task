namespace MeetupPlatformApi.Features.Meetups.UpdateMeetup;

using System.ComponentModel.DataAnnotations;

public class UpdateMeetupDto
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
