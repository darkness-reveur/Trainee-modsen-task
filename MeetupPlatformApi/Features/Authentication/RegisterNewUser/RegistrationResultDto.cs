namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using System.ComponentModel.DataAnnotations;

public class RegistrationResultDto
{
    [Required]
    public UserInfoDto UserInfo { get; set; }

    [Required]
    public TokenPairDto TokenPair { get; set; }
    
    public class UserInfoDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
