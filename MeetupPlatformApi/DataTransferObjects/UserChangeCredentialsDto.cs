using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects
{
    public class UserChangeCredentialsDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
