namespace MeetupPlatformApi.Domain.Users;

using MeetupPlatformApi.Authentication.Helpers;

public class PlainUser : User
{
    public override string Role => Roles.PlainUser;

    public Guid? MeetupId { get; set; }
}
