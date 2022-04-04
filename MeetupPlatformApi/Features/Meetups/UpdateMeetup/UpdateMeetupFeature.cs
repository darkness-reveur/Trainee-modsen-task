namespace MeetupPlatformApi.Features.Meetups.UpdateMeetup;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class UpdateMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public UpdateMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    [HttpPut("/api/meetups/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
