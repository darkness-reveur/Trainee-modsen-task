namespace MeetupPlatformApi.Features.Authentication.GetUser;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Authentication)]
public class GetUserFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetUserFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    [HttpGet("/api/users/{id:guid}")]
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
