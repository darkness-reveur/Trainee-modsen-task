namespace MeetupPlatformApi.Features.Meetups.DeleteMeetupContact;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class DeleteMeetupContactFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public DeleteMeetupContactFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Delete meetup contact.
    /// </summary>
    /// <response code="204">Returns if deleting was seccussful.</response>
    /// <response code="404">If needed meetup or contact are nulls.</response>
    [HttpDelete("/api/meetups/{meetupId:guid}/delete-contact/{contactId:guid}")]
    public async Task<IActionResult> GetMeetup([FromRoute] Guid meetupId, [FromRoute] Guid contactId)
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

