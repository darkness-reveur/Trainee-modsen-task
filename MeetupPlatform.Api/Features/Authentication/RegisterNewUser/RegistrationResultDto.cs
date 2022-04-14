namespace MeetupPlatform.Api.Features.Authentication.RegisterNewUser;

using System.ComponentModel.DataAnnotations;

public class RegistrationResultDto
{
    /// <summary>
    /// User info details
    /// </summary>
    [Required]
    public UserInfoDto UserInfo { get; set; }

    /// <summary>
    /// Access token JWT string
    /// </summary>
    /// <example>*JWT string*</example>
    [Required]
    public TokenPairDto TokenPair { get; set; }
    
    public class UserInfoDto
    {
        /// <summary>
        /// User id value
        /// </summary>
        /// <example>cc3e5bce-a8c0-4258-b663-71ec8f2b6446</example>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// User login name
        /// </summary>
        /// <example>Inan1965</example>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        /// <example>Organizer</example>
        [Required]
        public string Role { get; set; }
    }
}
