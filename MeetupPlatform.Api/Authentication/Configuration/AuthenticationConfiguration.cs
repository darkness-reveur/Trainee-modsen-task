namespace MeetupPlatform.Api.Authentication.Configuration;

using System.Text;
using MeetupPlatform.Api.Seedwork.Configuration;
using Microsoft.IdentityModel.Tokens;

public class AuthenticationConfiguration
{
    public SigningCredentials SigningCredentials { get; }
    
    public TimeSpan AccessTokenLifetime { get; }

    public TimeSpan RefreshTokenLifetime { get; }
    
    public TokenValidationParameters ValidationParameters { get; }

    public AuthenticationConfiguration(IConfiguration applicationConfiguration)
    {
        var authenticationConfiguration = applicationConfiguration.FromSection("Auth");

        var secretKey = authenticationConfiguration.GetRequiredString("SecretKey");
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
        var securityKey = new SymmetricSecurityKey(secretKeyBytes);
        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        
        var accessTokenLifetimeInMinutes = authenticationConfiguration.GetRequiredInt("AccessTokenLifetimeInMinutes");
        AccessTokenLifetime = TimeSpan.FromMinutes(accessTokenLifetimeInMinutes);

        var refreshTokenLifetimeInDays = authenticationConfiguration.GetRequiredInt("RefreshTokenLifetimeInDays");
        RefreshTokenLifetime = TimeSpan.FromDays(refreshTokenLifetimeInDays);

        ValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SigningCredentials.Key,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
}
