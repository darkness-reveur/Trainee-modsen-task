namespace MeetupPlatform.Api.Features.Meetups.LeftRootComment;

using System.ComponentModel.DataAnnotations;

public class CreationCommentDto
{
    /// <summary>
    /// Text of comment.
    /// </summary>
    /// <example>Hello my friend!</example>
    [Required]
    [MaxLength(4000)]
    public string Text { get; set; }
}
