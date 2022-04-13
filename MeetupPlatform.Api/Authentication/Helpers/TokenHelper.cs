namespace MeetupPlatform.Api.Authentication.Helpers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MeetupPlatform.Api.Authentication.Configuration;
using MeetupPlatform.Api.Domain.Users;
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

    /// <summary>Parses provided encoded refresh token.</summary>
    /// <param name="encodedRefreshToken">Encoded refresh token to parse.</param>
    /// <returns>Returns <see cref="RefreshTokenPayload"/> if token is valid, <c>null</c> otherwise.</returns>
    public RefreshTokenPayload ParseRefreshToken(string encodedRefreshToken)
    {
        try
        {
            var refreshToken = tokenHandler.ValidateToken(encodedRefreshToken, configuration.ValidationParameters, out _);

            var tokenIdClaim = refreshToken.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier);
            var tokenId = Guid.Parse(tokenIdClaim.Value);

            return new RefreshTokenPayload
            {
                TokenId = tokenId
            };
        }
        catch
        {
            return null;
        }
    }

    public TokenPair IssueTokenPair(User user, Guid refreshTokenId)
    {
        var accessToken = IssueToken(
            payload: new Dictionary<string, object>
            {
                {ClaimTypes.NameIdentifier, user.Id},
                {ClaimTypes.Role, user.Role }
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
