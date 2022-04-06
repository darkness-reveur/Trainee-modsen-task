﻿namespace MeetupPlatformApi.Features.Authentication.GetCurrentUserInfo;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Authentication)]
public class GetCurrentUserInfoFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetCurrentUserInfoFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get current user info.
    /// </summary>
    /// <response code="200">Returns current user info.</response>
    /// <response code="401">If the the user unauthorized.</response>
    /// <response code="403">If the the user don't have a permission.</response>
    /// <response code="500">If there are database interaction errors.</response>
    [HttpGet("/api/users/me")]
    [Authorize]
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrentUserInfo()
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);

        var userInfoDto = mapper.Map<UserInfoDto>(user);
        return Ok(userInfoDto);
    }
}
