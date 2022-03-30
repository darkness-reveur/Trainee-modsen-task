namespace MeetupPlatformApi.Authentification;

using MeetupPlatformApi.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetupPlatformApi.Extensions;

public class AuthenticationManager
{
    private readonly IConfiguration configuration;

    public AuthenticationManager(IConfiguration configuration)
    {
        this.configuration = configuration;
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
            Expires = DateTime.Now.Add(TimeSpan.FromMinutes(Convert.ToDouble(configuration.GetSectionValueFromJwt("Expire")))),
            Claims = claims
        };

        return new JwtSecurityTokenHandler().CreateToken(descriptor);
    }
}
