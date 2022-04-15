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
}
