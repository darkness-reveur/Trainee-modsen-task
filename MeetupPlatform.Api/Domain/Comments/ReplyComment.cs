namespace MeetupPlatform.Api.Domain.Comments;

public class ReplyComment
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public Guid RootCommentId { get; set; }
}
