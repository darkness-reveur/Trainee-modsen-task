namespace MeetupPlatformApi.Features.Authentication.RefreshTokenPair;

using AutoMapper;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
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

    [HttpPost("/api/users/refresh-tokens")]
    public async Task<IActionResult> RefreshTokenPair([FromBody] string refreshToken)
    {
        var refreshTokenExpires = tokenHelper.GetExpires(refreshToken);
        if (DateTime.UtcNow > refreshTokenExpires)
        {
            return BadRequest("Refresh token is expired.");
        }

        var refreshTokenId = tokenHelper.GetNameIdentifier(refreshToken);
        var refreshTokenInfo = await context.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Id == refreshTokenId);

        if (refreshTokenInfo is null)
        {
            return NotFound("Provided token is either fake or already was used.");
        }

        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == refreshTokenInfo.UserId);
        if (user is null)
        {
            return BadRequest("Token's user id is fake.");
        }

        var newRefreshTokenId = Guid.NewGuid();
        var tokenPair = tokenHelper.IssueTokenPair(user, newRefreshTokenId);
        var newRefreshToken = new RefreshToken()
        {
            Id = tokenHelper.GetNameIdentifier(tokenPair.RefreshToken),
            UserId = user.Id
        };
        context.RefreshTokens.Remove(refreshTokenInfo);
        await context.RefreshTokens.AddAsync(newRefreshToken);
        await context.SaveChangesAsync();

        var outputDto = mapper.Map<TokenPairDto>(tokenPair);
        return Ok(outputDto);
    }
}
