namespace MeetupPlatformApi.Features.Authentication.GetCurrentUserInfo;

using AutoMapper;
using MeetupPlatformApi.Authentication.Manager;
using MeetupPlatformApi.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Tags("Authentication")]
public class GetCurrentUserInfoFeature : ControllerBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;
    private readonly AuthenticationManager authenticationManager;

    public GetCurrentUserInfoFeature(ApplicationContext context, IMapper mapper, AuthenticationManager authenticationManager)
    {
        this.context = context;
        this.mapper = mapper;
        this.authenticationManager = authenticationManager;
    }

    [HttpGet("/api/users/me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserInfo()
    {
        var currentUserInfo = authenticationManager.GetCurrentUserInfo(User);
        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == currentUserInfo.UserId);

        var userInfoDto = mapper.Map<UserInfoDto>(user);
        return Ok(userInfoDto);
    }
}
