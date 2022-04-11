namespace MeetupPlatformApi.Features.Meetups.GetMeetup;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class GetMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get meetup by id.
    /// </summary>
    /// <response code="200">Returns meetup item.</response>
    /// <response code="404">If needed meetup is null.</response>
    [HttpGet("/api/meetups/{id:guid}")]
    [ProducesResponseType(typeof(MeetupInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMeetup([FromRoute] Guid id)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }

        var contacts = await context.Contacts.Include(c => c.Meetups).ToListAsync();
        var meetupContacts = contacts.SelectMany(u => u.Meetups,
            (u, l) => new { Contact = u, Meetup = l })
            .Where(u => u.Meetup.Id == id )
            .Select(u => u.Contact).ToList();

        var meetupInfoDto = mapper.Map<MeetupInfoDto>(meetup);
        meetupInfoDto.ContactInfo = mapper.Map<IList<ContactInfoDto>>(meetupContacts);
        return Ok(meetupInfoDto);
    }
}
