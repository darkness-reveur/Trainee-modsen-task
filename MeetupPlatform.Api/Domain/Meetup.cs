namespace MeetupPlatform.Api.Domain;

using MeetupPlatform.Api.Domain.Comments;
using MeetupPlatform.Api.Domain.Users;

public class Meetup
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public Guid OrganizerId { get; set; }

    public List<PlainUser> SignedUpUsers { get; }

    public List<RootComment> Comments { get; }
}
