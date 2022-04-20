namespace MeetupPlatform.Api.Features.Meetups.GetRootComments.Filter.FilterSettings;

using System.ComponentModel.DataAnnotations;

public class RootCommentFilterSettings
{
    /// <summary>
    /// Number of page.
    /// </summary>
    /// <example>3</example>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of comment that will fit on one page.
    /// </summary>
    /// <example>2</example>
    [Required]
    [Range(1, 100)]
    public int PageSize { get; set; }
}
