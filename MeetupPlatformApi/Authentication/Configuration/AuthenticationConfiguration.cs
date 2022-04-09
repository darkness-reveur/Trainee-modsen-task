namespace MeetupPlatformApi.Authentication.Configuration;

using System.Text;
using MeetupPlatformApi.Seedwork.Configuration;
using Microsoft.IdentityModel.Tokens;

public class AuthenticationConfiguration
{
    public SigningCredentials SigningCredentials { get; }
    
    public TimeSpan AccessTokenLifetime { get; }

    public TimeSpan RefreshTokenLifetime { get; }
    
    public TokenValidationParameters ValidationParameters { get; }

    public AuthenticationConfiguration(IConfiguration applicationConfiguration)
    {
        const string authSectionName = "Auth";

        var secretKey = applicationConfiguration.GetString($"{authSectionName}:SecretKey");
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
        var securityKey = new SymmetricSecurityKey(secretKeyBytes);
        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        
        var accessTokenLifetimeInMinutes = applicationConfiguration.GetInt($"{authSectionName}:AccessTokenLifetimeInMinutes");
        AccessTokenLifetime = TimeSpan.FromMinutes(accessTokenLifetimeInMinutes);

        var refreshTokenLifetimeInDays = applicationConfiguration.GetInt($"{authSectionName}:RefreshTokenLifetimeInDays");
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
