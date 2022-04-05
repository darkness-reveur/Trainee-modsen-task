namespace MeetupPlatformApi.Features.Authentication.RevokeUserRefreshTokens;

using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Authentication)]
public class RevokeUserRefreshTokensFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public RevokeUserRefreshTokensFeature(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpDelete("/api/users/me/refresh-tokens")]
    [Authorize]
    public async Task<IActionResult> RevokeUserRefreshTokens()
    {
        var user = await context.Users
            .Include(user => user.RefreshTokens)
            .SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);
        if (user is null)
        {
            return NotFound($"Provided user is either fake or already was used");
        }

        context.RefreshTokens.RemoveRange(user.RefreshTokens);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
