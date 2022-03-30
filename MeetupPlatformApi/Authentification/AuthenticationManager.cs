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
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(configuration.GetSectionValueFromJwt("SecretKey"));
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(UserEntity user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(Convert.ToDouble(configuration.GetSectionValueFromJwt("Expire")))),
            claims: claims);

        return tokenOptions;
    }
}
