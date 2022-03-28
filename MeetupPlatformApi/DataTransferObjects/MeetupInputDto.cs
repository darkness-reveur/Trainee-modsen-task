using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects;

public class MeetupInputDto
{
    [Required]
    [MaxLength(30)]
    [DataType(DataType.Text)]
    [Display(Name = "Name of meetup")]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? EndTime { get; set; }

    [DataType(DataType.Text)]
    [MaxLength(300)]
    [Display(Name = "Meetup description")]
    public string Description { get; set; }
}

