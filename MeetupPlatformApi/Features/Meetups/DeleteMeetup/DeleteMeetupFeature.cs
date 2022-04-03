namespace MeetupPlatformApi.Features.Meetups.DeleteMeetup;

using MeetupPlatformApi.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Tags("Meetups")]
public class DeleteMeetupFeature : ControllerBase
{
    private readonly ApplicationContext context;

    public DeleteMeetupFeature(ApplicationContext context) =>
        this.context = context;
    
    [HttpDelete("/api/meetups/{id:guid}")]
    public async Task<IActionResult> DeleteMeetup([FromRoute] Guid id)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }

        context.Meetups.Remove(meetup);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
