using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects;

public class MeetupOutputDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    [DataType(DataType.Text)]
    [Display(Name = "Name of meetup")]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime EndTime { get; set; }

    [Display(Name = "Meetup description")]
    [DataType(DataType.Text)]
    [MaxLength(300)]
    public string Description { get; set; }
}

