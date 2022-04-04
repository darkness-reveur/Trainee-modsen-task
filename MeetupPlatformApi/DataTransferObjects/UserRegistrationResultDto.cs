namespace MeetupPlatformApi.DataTransferObjects;

using System.ComponentModel.DataAnnotations;

public class UserRegistrationResultDto
{
    [Required]
    public UserOutputDto UserInfo { get; set; }

    [Required]
    public TokenPairDto TokenPair { get; set; }
}
