namespace MeetupPlatform.Api.Domain.Comments;

using MeetupPlatform.Api.Authentication.Helpers;

public class ReplyComment : Comment
{
    public override string CommentType => Comments.Reply;
}
