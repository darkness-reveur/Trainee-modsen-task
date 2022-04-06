namespace MeetupPlatformApi.Features.Authentication.ChangeUserCredentials;

using BCrypt.Net;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Authentication)]
public class ChangeUserCredentialsFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public ChangeUserCredentialsFeature(ApplicationContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Change user credentials.
    /// </summary>
    /// <response code="204">If the user credentials change was successful.</response>
    /// <response code="400">If the user is null.</response>
    /// <response code="404">If the username or password not valid.</response>
    [HttpPut("/api/users/me/credentials")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> ChangeCredentials([FromBody] UserCredentialsChangeDto credentialsChangeDto)
    {
        var user = await context.Users
            .Include(user => user.RefreshTokens)
            .SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);
        if(user is null)
        {
            return NotFound("User doesn't exist.");
        }

        var allowedUsername = credentialsChangeDto.Username == user.Username ? true
            : !await context.Users.AnyAsync(user => user.Username == credentialsChangeDto.Username);
        if (!BCrypt.Verify(credentialsChangeDto.OldPassword, user.Password) || !allowedUsername)
        {
            return BadRequest();
        }

        user.Password = BCrypt.HashPassword(credentialsChangeDto.NewPassword);
        user.Username = credentialsChangeDto.Username;
        user.RefreshTokens.Clear();
        await context.SaveChangesAsync();
        return NoContent();
    }
}
