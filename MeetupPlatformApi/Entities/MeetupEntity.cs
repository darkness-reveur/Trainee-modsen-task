namespace MeetupPlatformApi.Entities;

using System.ComponentModel.DataAnnotations;

public class MeetupEntity
{
    [Required]
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public string Description { get; set; }

    public string Location { get; set; }
}
