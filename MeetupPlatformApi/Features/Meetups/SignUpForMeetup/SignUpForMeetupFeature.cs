namespace MeetupPlatformApi.Features.Meetups.SignUpForMeetup;

using AutoMapper;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class SignUpForMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public SignUpForMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpPost("/api/meetups/sign-up/{id:guid}")]
    [Authorize(Roles = Roles.PlainUser)]
    public async Task<IActionResult> SignUpForMeetup([FromRoute] Guid id)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.Users)
            .SingleOrDefaultAsync(meetup => meetup.Id == id);
        if(meetup is null)
        {
            return NotFound();
        }

        var user = await context.PlainUsers.SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);
        if(user is null)
        {
            // We can't sign up non-existing user to meetup.
            return BadRequest();
        }

        if(user?.MeetupId == meetup.Id)
        {
            // We can't twice sign up the user to meetup.
            return BadRequest();
        }

        user.MeetupId = meetup.Id;
        await context.SaveChangesAsync();

        var meetupInfoDto = mapper.Map<MeetupInfoDto>(meetup);
        return Ok(meetupInfoDto);
    }
}
