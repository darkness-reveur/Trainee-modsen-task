namespace MeetupPlatform.Api.Domain.Comments;

public abstract class Comment
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public abstract string CommentType { get; } 
}
