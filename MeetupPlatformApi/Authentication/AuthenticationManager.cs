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

        return new() { UserId = id };
    }

    public TokenPair IssueTokenPair(UserEntity user)
    {
        var accessToken = IssueAccessToken(
            payload: new Dictionary<string, object>
            {
                    {ClaimTypes.NameIdentifier, user.Id}
            },
            accessTokenLifetime: configuration.AccessTokenLifetime);

        var refreshToken = IssueRefreshToken(configuration.RefreshTokenLifetime, user.Id);

        return new TokenPair
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string IssueAccessToken(IDictionary<string, object> payload, TimeSpan accessTokenLifetime)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = payload,
            Expires = DateTime.UtcNow.Add(accessTokenLifetime),
            SigningCredentials = configuration.SigningCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private RefreshTokenEntity IssueRefreshToken(TimeSpan refreshTokenLifetime, Guid userId)
    {
        return new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            Expires = DateTime.UtcNow.Add(refreshTokenLifetime),
            UserId = userId
        };
    }
}
