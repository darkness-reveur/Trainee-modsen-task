namespace MeetupPlatformApi.Features.Authentication.AuthenticateUser;

using BCrypt.Net;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
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
    /// User authentication in the system.
    /// </summary>
    /// <response code="200">Returns the newly created item.</response>
    /// <response code="400">If the user is null or password not valid.</response>
    /// <response code="500">If there are database interaction errors.</response>
    [HttpPost("/api/users/authenticate")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AuthenticateUser([FromBody] CredentialsDto credentialsDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Username == credentialsDto.Username);
        if (user is null || !BCrypt.Verify(credentialsDto.Password, user.Password))
        {
            return BadRequest("Username or password is incorrect.");
        }

        var accessToken = tokenHelper.IssueAccessToken(user);
        var tokenDto = new TokenDto { AccessToken = accessToken };
        return Ok(tokenDto);
    }
}
