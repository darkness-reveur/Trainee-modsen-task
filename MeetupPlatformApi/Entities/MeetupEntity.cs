using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.Entities;

public class MeetupEntity
{
    [Required]
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public string Description { get; set; }
}
