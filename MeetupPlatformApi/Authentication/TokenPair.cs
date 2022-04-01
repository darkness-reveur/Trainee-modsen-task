using MeetupPlatformApi.Entities;

namespace MeetupPlatformApi.Authentication
{
    public class TokenPair
    {
        public string AccessToken { get; set; }

        public Guid RefreshTokenId { get; set; }
}
}
