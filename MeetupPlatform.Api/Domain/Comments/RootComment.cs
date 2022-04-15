namespace MeetupPlatform.Api.Domain.Comments;

public class RootComment
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public Guid MeetupId { get; set; }

    public Guid AuthorId { get; set; }

    public List<ReplyComment> ReplyComments { get; }

    public DateTime Posted { get; set; }
}
