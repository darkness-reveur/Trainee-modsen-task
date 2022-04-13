namespace MeetupPlatformApi.Features.Meetups.UpdateMeetup;

using AutoMapper;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class UpdateMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public UpdateMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Update meetup by his id.
    /// </summary>
    /// <response code="204">If updating was successful.</response>
    /// <response code="404">If needed meetup is null.</response>
    [HttpPut("/api/meetups/{id:guid}")]
    [Authorize(Roles = Roles.Organizer)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMeetup([FromRoute] Guid id, [FromBody] UpdateMeetupDto updateDto)
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

        mapper.Map(updateDto, meetup);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
