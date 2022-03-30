using MeetupPlatformApi.Context;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetupPlatformApi.Extensions;

namespace MeetupPlatformApi.Authentification;

public class AuthenticationManager
{
    private readonly ApplicationContext context;
    private readonly IConfiguration configuration;

    private UserEntity user;

    public AuthenticationManager(ApplicationContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    public async Task<bool> ValidateUser(UserAuthenticationDto userForAuth)
    {
        user = await context.Users.SingleOrDefaultAsync(u => u.Username.Equals(userForAuth.Username));

        return (user != null && BCrypt.Net.BCrypt.Verify(userForAuth.Password, user.Password));
    }

    public string CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(configuration.GetSectionValueFromJwt("SecretKey"));
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims()
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
