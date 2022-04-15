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

    /// <summary>
    /// Post's datetime.
    /// </summary>
    /// <example>2022-04-04T21:49:33.985Z</example>
    [Required]
    public DateTime Posted { get; set; }
}
