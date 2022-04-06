namespace MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.FilterSettings;

using System.ComponentModel.DataAnnotations;

public class MeetupsFilterSettings
{
    [Required]
    [Range(1,100000)]
    public int PageNumber { get; set; }
    
    [Required]
    [Range(1, 100, ErrorMessage = "too much data")]
    public int PageSize { get; set; }

    [Required]
    public SortOptions SortOptions { get; set; }

    public string SearchString { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string Location { get; set; }
}
