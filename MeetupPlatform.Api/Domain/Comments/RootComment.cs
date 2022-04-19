namespace MeetupPlatform.Api.Domain.Comments;

public class RootComment : Comment
{
    public Guid? MeetupId { get; set; }

    public List<ReplyComment> ReplyComments { get; }
}
