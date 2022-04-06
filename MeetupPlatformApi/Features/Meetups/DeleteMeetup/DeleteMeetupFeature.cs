namespace MeetupPlatformApi.Features.Meetups.DeleteMeetup;

using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class DeleteMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public DeleteMeetupFeature(ApplicationContext context) =>
        this.context = context;
    
    [HttpDelete("/api/meetups/{id:guid}")]
    [Authorize(Roles = Roles.Organizer)]
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
