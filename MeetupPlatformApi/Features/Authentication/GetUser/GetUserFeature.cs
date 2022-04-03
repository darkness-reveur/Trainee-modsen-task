namespace MeetupPlatformApi.Features.Authentication.GetUser;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Tags("Authentication")]
public class GetUserFeature : ControllerBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetUserFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    [HttpGet("/api/users/{id:guid}", Name = "GetUser")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == id);
        if (user is null)
        {
            return NotFound();
        }
        
        var userInfoDto = mapper.Map<UserInfoDto>(user);
        return Ok(userInfoDto);
    }
}
