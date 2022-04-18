namespace MeetupPlatform.Api.Features.Meetups.Contacts.DeleteContact;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class DeleteContactFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public DeleteContactFeature(ApplicationContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Delete contact information from meetup.
    /// </summary>
    /// <response code="404">If needed meetup or contact is null.</response>
    /// <response code="204">Returns when contact was deleted successfully.</response>
    [HttpDelete("/api/meetups/{meetupId:guid}/contacts/{contactId:guid}")]
    [Authorize(Roles = Roles.Organizer)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid meetupId, [FromRoute] Guid contactId)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.Contacts)
            .Where(meetup => meetup.Id == meetupId)
            .SingleOrDefaultAsync();
        if (meetup is null)
        {
            return NotFound();
        }

        var contact = meetup.Contacts.Where(contact => contact.Id == contactId).SingleOrDefault();
        if (contact is null)
        {
            return NotFound();
        }

        context.Contacts.Remove(contact);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
