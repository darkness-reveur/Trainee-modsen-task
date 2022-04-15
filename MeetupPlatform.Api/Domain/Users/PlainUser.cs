namespace MeetupPlatform.Api.Domain.Users;

using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain.Comments;

public class PlainUser : User
{
    public override string Role => Roles.PlainUser;

    public List<Meetup> MeetupsSignedUpFor { get; set; }

    public List<RootComment> RootComments { get; set; }

    public List<ReplyComment> ReplyComments { get; set; }
}
