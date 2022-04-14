namespace MeetupPlatform.Api.Domain.Comments;

public class ReplyComment : Comment
{
    public Guid RootCommentId { get; set; }
}
