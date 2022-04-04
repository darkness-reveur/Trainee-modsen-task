namespace MeetupPlatformApi.Entities;

public class MeetupEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Description { get; set; }
}
