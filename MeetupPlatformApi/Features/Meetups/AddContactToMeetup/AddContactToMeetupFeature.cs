namespace MeetupPlatformApi.Features.Meetups.AddContactToMeetup;

using AutoMapper;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class AddContactToMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public AddContactToMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Add contact to a meetup.
    /// </summary>
    /// <response code="204">If addition was successful.</response>
    /// <response code="404">If needed meetup is null.</response>
    [HttpPost("/api/meetups/{id:guid}/add-contact")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddContactToMeetup([FromRoute] Guid id, [FromBody] MeetupContactAddDto meetupContactAddDto)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }

        var contact = mapper.Map<Contact>(meetupContactAddDto);
        context.Contacts.Add(contact);
        meetup.Contacts.Add(contact);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
