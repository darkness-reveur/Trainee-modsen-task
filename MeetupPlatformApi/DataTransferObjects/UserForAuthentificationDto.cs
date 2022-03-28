using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects
{
    public class UserForAuthentificationDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
