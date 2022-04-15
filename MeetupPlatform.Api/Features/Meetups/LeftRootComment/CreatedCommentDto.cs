namespace MeetupPlatform.Api.Features.Meetups.LeftRootComment;

using System.ComponentModel.DataAnnotations;

public class CreatedCommentDto
{
    /// <summary>
    /// Comment id value.
    /// </summary>
    /// <example>cc3e5bce-a8c0-4258-b663-71ec8f2b6446</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Comment text.
    /// </summary>
    /// <example>You're gorgeous!</example>
    [Required]
    public string Text { get; set; }
}
