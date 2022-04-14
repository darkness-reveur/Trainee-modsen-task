namespace MeetupPlatform.Api.Domain.Comments;

using MeetupPlatform.Api.Authentication.Helpers;

public class RootComment : Comment
{
    public override string CommentType => Comments.Root;
}
