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

    [HttpPost("/api/users/me/refresh-tokens")]
    [Authorize]
    public async Task<IActionResult> RefreshTokenPair([FromBody] string refreshToken)
    {
        var refreshTokenExpires = tokenHelper.GetExpires(refreshToken);
        if (tokenHelper.IsExpired(refreshToken))
        {
            return BadRequest();
        }

        var refreshTokenId = tokenHelper.GetNameIdentifier(refreshToken);
        var refreshTokenInfo = await context.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Id == refreshTokenId);
        if (refreshTokenInfo is null)
        {
            return BadRequest();
        }

        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == refreshTokenInfo.UserId);
        if (user is null)
        {
            return BadRequest();
        }

        var newRefreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id
        };
        context.RefreshTokens.Remove(refreshTokenInfo);
        context.RefreshTokens.Add(newRefreshToken);
        await context.SaveChangesAsync();

        var tokenPair = tokenHelper.IssueTokenPair(user, newRefreshToken.Id);
        var refreshTokenDto = mapper.Map<TokenPairDto>(tokenPair);
        return Ok(refreshTokenDto);
    }
}
