namespace MeetupPlatformApi.Features.Meetups.RemoveMeetupContact;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class RemoveMeetupContactFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public RemoveMeetupContactFeature(ApplicationContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Remove contact from meetup.
    /// </summary>
    /// <response code="204">Returns if deleting was seccussful.</response>
    /// <response code="404">If needed meetup or contact are nulls.</response>
    [HttpDelete("/api/meetups/{meetupId:guid}/remove-contact/{contactId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveMeetupContact([FromRoute] Guid meetupId, [FromRoute] Guid contactId)
    {
        var meetup = await context.Meetups.Include(items => items.Contacts).SingleOrDefaultAsync(meetup => meetup.Id == meetupId);
        if (meetup is null)
        {
            return NotFound();
        }
        var contact = await context.Contacts.SingleOrDefaultAsync(contact => contact.Id == contactId);
        if (contact is null)
        {
            return NotFound();
        }

        meetup.Contacts.Remove(contact);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
