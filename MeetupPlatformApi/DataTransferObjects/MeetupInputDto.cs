namespace MeetupPlatformApi.DataTransferObjects;

using System.ComponentModel.DataAnnotations;

public class MeetupInputDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    [MaxLength(4000)]
    public string Description { get; set; }

    [Required]
    public string Location { get; set; }
}
