namespace MeetupPlatformApi.Domain;

using MeetupPlatformApi.Domain.Users;

public class Meetup
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Description { get; set; }

    public Guid OrganizerId { get; set; }

    public List<PlainUser> Users { get; set; }
}
