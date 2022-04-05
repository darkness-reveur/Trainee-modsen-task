﻿namespace MeetupPlatformApi.Features.Authentication.AuthenticateUser;

using BCrypt.Net;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Domain;
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

    [HttpPost("/api/users/authenticate")]
    public async Task<IActionResult> AuthenticateUser([FromBody] CredentialsDto credentialsDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Username == credentialsDto.Username);
        if (user is null || !BCrypt.Verify(credentialsDto.Password, user.Password))
        {
            return BadRequest("Username or password is incorrect.");
        }

        var refreshTokenId = Guid.NewGuid();
        var tokenPair = tokenHelper.IssueTokenPair(user, refreshTokenId);
        var refreshToken = new RefreshToken()
        {
            Id = refreshTokenId,
            UserId = user.Id
        };
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();

        var tokenPairDto = new TokenPairDto { AccessToken = tokenPair.AccessToken, RefreshToken = tokenPair.RefreshToken };
        return Ok(tokenPairDto);
    }
}
