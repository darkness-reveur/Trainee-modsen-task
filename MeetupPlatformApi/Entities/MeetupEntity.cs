namespace MeetupPlatformApi.Entities;

using System.ComponentModel.DataAnnotations;

public class MeetupEntity
{
    [Required]
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Description { get; set; }
}
