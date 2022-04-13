namespace MeetupPlatform.Api.Domain.Users;

using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain;

public class Organizer : User
{
    public override string Role => Roles.Organizer;

    public List<Meetup> OrganizedMeetups { get; set; }
}
