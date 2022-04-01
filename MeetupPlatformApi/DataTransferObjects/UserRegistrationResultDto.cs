using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects
{
    public class UserRegistrationResultDto
    {
        [Required]
        public UserOutputDto UserInfo { get; set; }

        [Required]
        public AuthenticationTokenPairOutputDto TokenPair { get; set; }
    }
}
