namespace MeetupPlatformApi.Domain.Users;

using MeetupPlatformApi.Authentication.Helpers;

public class Organizer : User
{
    public override string Role => Roles.Organizer;

    public List<Meetup> OrganizedMeetups { get; set; }
}
