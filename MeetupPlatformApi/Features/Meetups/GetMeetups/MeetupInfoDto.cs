namespace MeetupPlatformApi.Features.Meetups.GetMeetups;

using System.ComponentModel.DataAnnotations;

public class MeetupInfoDto
{
    /// <summary>
    /// Meetup id value
    /// </summary>
    /// <example>cc3e5bce-a8c0-4258-b663-71ec8f2b6446</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Meetup title
    /// </summary>
    /// <example>Work meeting</example>
    [Required]
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
    public string Description { get; set; }

    /// <summary>
    /// Meetup users count
    /// </summary>
    /// <example>1</example>
    [Required]
    public int SignedUpUsersCount { get; set; }

    /// Meetup location 
    /// </summary>
    /// <example>Belarus, Mogilev, Lenina 29-40</example>
    [Required]
    public string Location { get; set; }
}
