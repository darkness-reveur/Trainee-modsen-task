namespace MeetupPlatformApi.Domain;

public class MeetupKeyword
{
    public Guid Id { get; set; }

    public Guid MeetupId { get; set; }
    public Meetup Meetup { get; set; }

    public Guid KeywordId { get; set; }
    public Keyword Keyword { get; set; }
}
