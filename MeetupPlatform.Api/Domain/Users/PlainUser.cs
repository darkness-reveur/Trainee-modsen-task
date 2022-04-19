namespace MeetupPlatform.Api.Domain.Users;

using MeetupPlatform.Api.Authentication.Helpers;

public class PlainUser : User
{
    public override string Role => Roles.PlainUser;

    public List<Meetup> MeetupsSignedUpFor { get; set; }
}
