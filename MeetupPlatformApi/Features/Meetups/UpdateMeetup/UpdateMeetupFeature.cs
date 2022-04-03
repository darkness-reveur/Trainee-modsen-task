namespace MeetupPlatformApi.Features.Meetups.UpdateMeetup;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Tags("Meetups")]
public class UpdateMeetupFeature : ControllerBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public UpdateMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    [HttpPut("/api/meetups/{id:guid}")]
    public async Task<IActionResult> UpdateMeetup([FromRoute] Guid id, [FromBody] UpdateMeetupDto updateDto)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }

        mapper.Map(updateDto, meetup);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
