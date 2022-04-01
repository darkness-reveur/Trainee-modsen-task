namespace MeetupPlatformApi.Authentication;

using MeetupPlatformApi.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class AuthenticationManager
{
    private readonly AuthenticationConfiguration configuration;
    private readonly JwtSecurityTokenHandler tokenHandler;

    public AuthenticationManager(AuthenticationConfiguration configuration)
    {
        this.configuration = configuration;
        tokenHandler = new JwtSecurityTokenHandler();
    }

    public CurrentUserInfo GetCurrentUserInfo(ClaimsPrincipal user)
    {
        var idClaim = user.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier);
        var id = Guid.Parse(idClaim.Value);

        return new() {UserId = id};
    }

    public TokenPair IssueAccessToken(UserEntity user) =>
        IssueTokenPair(
            payload: new Dictionary<string, object>
            {
                {ClaimTypes.NameIdentifier, user.Id}
            },
            accessTokenLifetime: configuration.AccessTokenLifetime);

    private TokenPair IssueTokenPair(IDictionary<string, object> payload, TimeSpan accessTokenLifetime, TimeSpan refreshTokenLifetime, Guid userId)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = payload,
            Expires = DateTime.UtcNow.Add(accessTokenLifetime),
            SigningCredentials = configuration.SigningCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        string accessToken = tokenHandler.WriteToken(token);

        var refreshToken = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            Expires = DateTime.UtcNow.Add(refreshTokenLifetime),
            UserId = userId
        };

        return new() { AccessToken = accessToken, RefreshTokenId = refreshToken.Id };
    }
}
