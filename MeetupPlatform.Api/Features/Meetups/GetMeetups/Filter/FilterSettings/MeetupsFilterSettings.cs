namespace MeetupPlatform.Api.Features.Meetups.GetMeetups.Filter.FilterSettings;
    
using System.ComponentModel.DataAnnotations;

public class MeetupsFilterSettings
{
    /// <summary>
    /// Number of page
    /// </summary>
    /// <example>2</example>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of meetups that will fit on one page
    /// </summary>
    /// <example>10</example>
    [Required]
    [Range(1, 100)]
    public int PageSize { get; set; }

    /// <summary>
    /// Parameter by which to sort meetups
    /// </summary>
    /// <example>AscendingStartTime</example>
    [Required]
    public SortOptions SortOption { get; set; }

    /// <summary>
    /// Parameter by which meetups are filtered by searching for similar words in the name and description
    /// </summary>
    /// <example>Super meetup</example>
    public string SearchString { get; set; }

    /// <summary>
    /// Meetup start time with time zone, to filter
    /// </summary>
    /// <example>2022-04-04T21:49:33.985Z</example>
    public DateTime? BottomBoundOfStartTime { get; set; }

    /// <summary>
    /// Meetup start time with time zone, to filter
    /// </summary>
    /// <example>2022-04-04T21:49:33.985Z</example>
    public DateTime? UpperBoundOfStartTime { get; set; }

    /// <summary>
    /// Meetup location, to filter
    /// </summary>
    /// <example>Belarus, Mogilev, Lenina 29-40</example>
    public string Location { get; set; }
}
