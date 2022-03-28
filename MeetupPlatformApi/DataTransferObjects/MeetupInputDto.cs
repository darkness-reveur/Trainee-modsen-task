using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects;

public class MeetupInputDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public string Description { get; set; }
}
