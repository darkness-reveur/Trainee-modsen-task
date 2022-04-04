namespace MeetupPlatformApi.Filter.FilterSettings;

using System.ComponentModel.DataAnnotations;

public class MeetupsFilterSettings
{
    [Required]
    public int PageNumber { get; set; }
    
    [Required]
    public int PageSize { get; set; }

    [Required]
    public bool IsDescendingSort { get; set; } 

    public string SearchString { get; set; } 

    public DateTime? StartTime { get; set; }

    public string Location { get; set; }

}

