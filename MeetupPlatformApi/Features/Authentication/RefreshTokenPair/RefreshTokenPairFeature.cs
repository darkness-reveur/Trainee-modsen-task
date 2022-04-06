namespace MeetupPlatformApi.Features.Authentication.RefreshTokenPair;

using AutoMapper;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Authentication)]
public class RefreshTokenPairFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;
    private readonly TokenHelper tokenHelper;

    public RefreshTokenPairFeature(ApplicationContext context, IMapper mapper, TokenHelper tokenHelper)
    {
        this.context = context;
        this.mapper = mapper;
        this.tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Refresh user token pair.
    /// </summary>
    /// <response code="200">Returns the newly created item.</response>
    /// <response code="400">If the user is null or token is either invalid or fake.</response>
    [HttpPost("/api/users/me/refresh-tokens")]
    [Authorize]
    [ProducesResponseType(typeof(TokenPairDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshTokenPair([FromBody] string encodedOldRefreshToken)
    {
        var refreshTokenPayload = tokenHelper.ParseRefreshToken(encodedOldRefreshToken);
        if (refreshTokenPayload is null)
        {
            // Token is either invalid or fake.
            return BadRequest();
        }

        var oldRefreshToken = await context.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Id == refreshTokenPayload.TokenId);
        if (oldRefreshToken is null)
        {
            // The token has either already been used or is fake.
            return BadRequest();
        }

        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == oldRefreshToken.UserId);
        if (user is null)
        {
            // We can't issue new tokens for non-existing users.
            return BadRequest();
        }

        var newRefreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id
        };
        context.RefreshTokens.Remove(oldRefreshToken);
        context.RefreshTokens.Add(newRefreshToken);
        await context.SaveChangesAsync();

        var tokenPair = tokenHelper.IssueTokenPair(user, newRefreshToken.Id);
        var refreshTokenDto = mapper.Map<TokenPairDto>(tokenPair);
        return Ok(refreshTokenDto);
    }
}
