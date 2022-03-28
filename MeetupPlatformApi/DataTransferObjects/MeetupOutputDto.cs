using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects;

public class MeetupOutputDto
{
    [Required]
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
