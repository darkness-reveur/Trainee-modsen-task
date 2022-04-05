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

    [HttpPut("/api/users/me/credentials")]
    [Authorize]
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
        context.RefreshTokens.RemoveRange(user.RefreshTokens);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
