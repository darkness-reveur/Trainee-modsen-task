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
        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);
        var unallowedUsername = credentialsChangeDto.Username == user.Username ? false
            : await context.Users.AnyAsync(user => user.Username == credentialsChangeDto.Username);

        if (user is null || !BCrypt.Verify(credentialsChangeDto.OldPassword, user.Password) || unallowedUsername)
        {
            return BadRequest($"User with username: {credentialsChangeDto.Username} is either fake or password is incorrect.");
        }

        var userRefreshTokens = await context.RefreshTokens.Where(token => token.UserId == user.Id).ToListAsync();

        user.Password = BCrypt.HashPassword(credentialsChangeDto.NewPassword);
        user.Username = credentialsChangeDto.Username;
        context.RefreshTokens.RemoveRange(userRefreshTokens);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
