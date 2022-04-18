namespace MeetupPlatform.Api.Features.Meetups.Contacts.UpdateContact;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class UpdateContactFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public UpdateContactFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Update contact information for a meetup.
    /// </summary>
    /// <response code="404">If needed meetup or contact is null.</response>
    /// <response code="204">Returns when contact was updated successfully.</response>
    [HttpPut("/api/meetups/{meetupId:guid}/contacts/{contactId:guid}")]
    [Authorize(Roles = Roles.Organizer)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateContact([FromRoute] Guid meetupId, [FromRoute] Guid contactId,
        [FromBody] UpdateContactDto updateContactDto)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.Contacts)
            .Where(meetup => meetup.Id == meetupId)
            .SingleOrDefaultAsync();
        if(meetup is null)
        {
            return NotFound();
        }

        var contact = meetup.Contacts.Where(contact => contact.Id == contactId).SingleOrDefault();
        if(contact is null)
        {
            return NotFound();
        }

        mapper.Map(updateContactDto, contact);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
