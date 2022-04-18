namespace MeetupPlatform.Api.Domain;

public class Contact
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Value { get; set; }

    public List<Meetup> Meetups { get; set; } = new List<Meetup>();
}
