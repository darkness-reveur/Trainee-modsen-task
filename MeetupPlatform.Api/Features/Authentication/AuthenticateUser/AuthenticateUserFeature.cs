namespace MeetupPlatform.Api.Features.Authentication.AuthenticateUser;

using BCrypt.Net;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Authentication)]
public class AuthenticateUserFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly TokenHelper tokenHelper;

    public AuthenticateUserFeature(ApplicationContext context, TokenHelper tokenHelper)
    {
        this.context = context;
        this.tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Authenticate user in the system.
    /// </summary>
    /// <response code="200">Returns the newly created item.</response>
    /// <response code="400">If the user is null or password not valid.</response>
    [HttpPost("/api/users/authenticate")]
    [ProducesResponseType(typeof(TokenPairDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AuthenticateUser([FromBody] CredentialsDto credentialsDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Username == credentialsDto.Username);
        if (user is null || !BCrypt.Verify(credentialsDto.Password, user.Password))
        {
            return BadRequest("Username or password is incorrect.");
        }

        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id
        };
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        var tokenPair = tokenHelper.IssueTokenPair(user, refreshToken.Id);
        var tokenPairDto = new TokenPairDto { AccessToken = tokenPair.AccessToken, RefreshToken = tokenPair.RefreshToken };
        return Ok(tokenPairDto);
    }
}
