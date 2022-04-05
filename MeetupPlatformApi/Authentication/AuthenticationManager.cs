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

    public Guid GetNameIdentifier(string refreshToken)
    {
        var token = tokenHandler.ReadJwtToken(refreshToken);
        return Guid.Parse(token.Claims.Single(claim => claim.Type == "nameid").Value);
    }

    public TokenPair IssueTokenPair(UserEntity user, Guid refreshTokenId)
    {
        var accessToken = IssueToken(
            payload: new Dictionary<string, object>
            {
                {ClaimTypes.NameIdentifier, user.Id}
            },
            tokenLifetime: configuration.AccessTokenLifetime);

        var refreshToken = IssueToken(
            payload: new Dictionary<string, object>
            {
                {ClaimTypes.NameIdentifier, refreshTokenId }
            },
            tokenLifetime: configuration.RefreshTokenLifetime);

        return new TokenPair
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string IssueToken(IDictionary<string, object> payload, TimeSpan tokenLifetime)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = payload,
            Expires = DateTime.UtcNow.Add(tokenLifetime),
            SigningCredentials = configuration.SigningCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}