namespace MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.FilterSettings;

using MeetupPlatformApi.Features.Meetups.MainFilter.FilterSettings;
using System.ComponentModel.DataAnnotations;

public class MeetupsFilterSettings : BaseFilterSettings
{
    [Required]
    public int PageNumber { get; set; }
    
    [Required]
    public int PageSize { get; set; }

    [Required]
    public bool IsDescendingDateSort { get; set; } 

}

