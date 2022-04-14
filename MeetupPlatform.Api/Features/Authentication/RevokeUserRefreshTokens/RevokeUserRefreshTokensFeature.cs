namespace MeetupPlatform.Api.Features.Authentication.RevokeUserRefreshTokens;

using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
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

    /// <summary>
    /// Revoke all user refresh tokens.
    /// </summary>
    /// <response code="204">If the revoke operation was successful.</response>
    /// <response code="404">If the user is null.</response>
    [HttpDelete("/api/users/me/refresh-tokens")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RevokeUserRefreshTokens()
    {
        var user = await context.Users
            .Include(user => user.RefreshTokens)
            .SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);
        if (user is null)
        {
            return NotFound();
        }

        user.RefreshTokens.Clear();
        await context.SaveChangesAsync();
        return NoContent();
    }
}
