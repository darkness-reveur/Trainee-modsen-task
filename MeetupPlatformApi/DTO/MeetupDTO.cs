using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DTO
{
    public class MeetupDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public int? CountOfVisitors { get; set; }

        public string Description { get; set; }
    }
}
