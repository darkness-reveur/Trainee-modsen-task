namespace MeetupPlatformApi.Features.Meetups.GetMeetup;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Tags("Meetups")]
public class GetMeetupFeature : ControllerBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    [HttpGet("/api/meetups/{id:guid}", Name = "GetMeetup")]
    public async Task<IActionResult> GetMeetup([FromRoute] Guid id)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }
        
        var meetupInfoDto = mapper.Map<MeetupInfoDto>(meetup);
        return Ok(meetupInfoDto);
    }
}
