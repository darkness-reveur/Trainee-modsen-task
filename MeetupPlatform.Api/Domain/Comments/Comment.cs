namespace MeetupPlatform.Api.Domain.Comments;

public abstract class Comment
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public Guid AuthorId { get; set; }

    public DateTime Posted { get; set; }
}
