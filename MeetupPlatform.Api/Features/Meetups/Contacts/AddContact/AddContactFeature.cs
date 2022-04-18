namespace MeetupPlatform.Api.Features.Meetups.Contacts.AddContact;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class AddContactFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public AddContactFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Add contact information for a meetup.
    /// </summary>
    /// <response code="404">If needed meetup is null.</response>
    /// <response code="201">Returns information about added contact.</response>
    [HttpPost("/api/meetups/{id:guid}/contacts")]
    [Authorize(Roles = Roles.Organizer)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(AddedContactDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddContact([FromRoute] Guid id, [FromBody] AdditionContactDto additionContactDto)
    {
        var meetup = await context.Meetups.Where(meetup => meetup.Id == id).SingleOrDefaultAsync();
        if(meetup is null)
        {
            return NotFound();
        }

        if(meetup.OrganizerId != CurrentUser.UserId)
        {
            return Forbid();
        }

        var contact = mapper.Map<Contact>(additionContactDto);
        contact.MeetupId = id;
        context.Contacts.Add(contact);
        await context.SaveChangesAsync();

        var addedContactDto = mapper.Map<AddedContactDto>(contact);
        return Created(addedContactDto);
    }
}
