namespace MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.FilterSettings;

using System.ComponentModel.DataAnnotations;

public class MeetupsFilterSettings
{
    [Required]
    [Range(1,int.MaxValue)]
    public int PageNumber { get; set; }
    
    [Required]
    [Range(1, 100)]
    public int PageSize { get; set; }

    [Required]
    public SortOptions SortOption { get; set; }

    public string SearchString { get; set; }

    public DateTime? BottomBoundOfStartTime { get; set; }

    public DateTime? UpperBoundOfStartTime { get; set; }

    public string Location { get; set; }
}
