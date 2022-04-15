namespace MeetupPlatform.Api.Features.Meetups.LeftReplyComment;

using System.ComponentModel.DataAnnotations;

public class CreationReplyDto
{
    /// <summary>
    /// Text of comment.
    /// </summary>
    /// <example>Hello! How are you?</example>
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
