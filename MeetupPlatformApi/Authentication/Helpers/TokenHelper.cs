namespace MeetupPlatformApi.Authentication.Helpers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MeetupPlatformApi.Authentication.Configuration;
using MeetupPlatformApi.Domain;
using Microsoft.IdentityModel.Tokens;

public class TokenHelper
{
    private readonly AuthenticationConfiguration configuration;
    private readonly JwtSecurityTokenHandler tokenHandler;

    public TokenHelper(AuthenticationConfiguration configuration)
    {
        this.configuration = configuration;
        tokenHandler = new JwtSecurityTokenHandler();
    }

    public Guid GetNameIdentifier(string refreshToken)
    {
        var token = tokenHandler.ReadJwtToken(refreshToken);
        return Guid.Parse(token.Claims.Single(claim => claim.Type == "nameid").Value);
    }

    public DateTime GetExpires(string refreshToken)
    {
        var token = tokenHandler.ReadJwtToken(refreshToken);
        return token.ValidTo;
    }

    public TokenPair IssueTokenPair(User user, Guid refreshTokenId)
    {
        var accessToken = IssueToken(
            payload: new Dictionary<string, object>
            {
                {ClaimTypes.NameIdentifier, user.Id}
            },
            lifetime: configuration.AccessTokenLifetime);

        var refreshToken = IssueToken(
            payload: new Dictionary<string, object>
            {
                {ClaimTypes.NameIdentifier, refreshTokenId }
            },
            lifetime: configuration.RefreshTokenLifetime);

        return new TokenPair
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string IssueToken(IDictionary<string, object> payload, TimeSpan lifetime)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = payload,
            Expires = DateTime.UtcNow.Add(lifetime),
            SigningCredentials = configuration.SigningCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}