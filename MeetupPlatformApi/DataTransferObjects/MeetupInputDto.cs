using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects;

public class MeetupInputDto
{
    [Required]
    [MaxLength(30)]
    [Display(Name = "Name of meetup")]
    public string Name { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [MaxLength(300)]
    [Display(Name = "Meetup description")]
    public string Description { get; set; }
}

