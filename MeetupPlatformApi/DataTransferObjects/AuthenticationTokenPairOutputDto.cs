using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects
{
    public class AuthenticationTokenPairOutputDto
    {
        [Required]
        public string AccessToken { get; set; }

        public Guid RefreshTokenId { get; set; }
    }
}
