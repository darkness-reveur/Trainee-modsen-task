namespace MeetupPlatformApi.Features.Meetups.DeleteMeetup;

using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class DeleteMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public DeleteMeetupFeature(ApplicationContext context) =>
        this.context = context;
    
    [HttpDelete("/api/meetups/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
