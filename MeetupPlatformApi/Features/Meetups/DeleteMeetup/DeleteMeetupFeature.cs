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

    /// <summary>
    /// Delete meetup by id.
    /// </summary>
    /// <response code="204">If deleting was successful.</response>
    /// <response code="404">If needed meetup is null.</response>
    [HttpDelete("/api/meetups/{id:guid}")]
    [Authorize(Roles = Roles.Organizer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMeetup([FromRoute] Guid id)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }

        if (meetup.OrganizerId != CurrentUser.UserId)
        {
            return Forbid();
        }

        context.Meetups.Remove(meetup);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
