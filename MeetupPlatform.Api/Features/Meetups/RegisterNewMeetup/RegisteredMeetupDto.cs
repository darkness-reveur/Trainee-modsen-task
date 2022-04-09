﻿namespace MeetupPlatform.Api.Features.Meetups.RegisterNewMeetup;

using System.ComponentModel.DataAnnotations;

public class RegisteredMeetupDto
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
}
