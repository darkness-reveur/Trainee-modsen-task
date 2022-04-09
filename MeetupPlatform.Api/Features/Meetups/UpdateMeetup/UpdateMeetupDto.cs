namespace MeetupPlatform.Api.Features.Meetups.UpdateMeetup;

using System.ComponentModel.DataAnnotations;

public class UpdateMeetupDto
{
    /// <summary>
    /// Meetup title
    /// </summary>
    /// <example>Work meeting</example>
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    /// <summary>
    /// Meetup start time with time zone
    /// </summary>
    /// <example>2022-04-04T21:49:33.985Z</example>
    [Required]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Meetup end time with time zone
    /// </summary>
    /// <example>2022-04-04T21:49:33.985Z</example>
    [Required]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Meetup description
    /// </summary>
    /// <example>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</example>
    [Required]
    [MaxLength(4000)]
    public string Description { get; set; }
}
