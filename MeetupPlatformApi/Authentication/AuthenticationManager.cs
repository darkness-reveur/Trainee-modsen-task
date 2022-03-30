namespace MeetupPlatformApi.Authentication;

using MeetupPlatformApi.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetupPlatformApi.Extensions;
using MeetupPlatformApi.Models;

public class AuthenticationManager
{
    private readonly IConfiguration configuration;

    public AuthenticationManager(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public AccessTokenPayload GetAccessTokenPayload(ClaimsPrincipal user)
    {
        var userNameIdentifier = user.FindFirst(ClaimTypes.NameIdentifier).Value;
        var userId = Guid.Parse(userNameIdentifier);

        return new AccessTokenPayload { UserId = userId };
    }

    public string CreateToken(UserEntity user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);
        var token = CreateToken(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(configuration.GetSectionValueFromJwt("SecretKey"));
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private Dictionary<string, object> GetClaims(UserEntity user)
    {
        var claims = new Dictionary<string, object>
            {
                { ClaimTypes.NameIdentifier, user.Id }
            };

        return claims;
    }

    private SecurityToken CreateToken(SigningCredentials signingCredentials, Dictionary<string, object> claims)
    {
        var descriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(Convert.ToInt32(configuration.GetSectionValueFromJwt("AccessTokenLifetimeInMinutes")))),
            Claims = claims
        };

        return new JwtSecurityTokenHandler().CreateToken(descriptor);
    }
}
