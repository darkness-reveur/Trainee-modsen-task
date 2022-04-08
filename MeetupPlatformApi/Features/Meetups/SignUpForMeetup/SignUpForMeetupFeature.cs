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

    /// <summary>
    /// Sign up for meetup by its id.
    /// </summary>
    /// <response code="404">If needed meetup is null.</response>
    /// <response code="400">If user doesn't exist or the user is trying to sign up for meetup twice.</response>
    /// <response code="200">If there's not errors after executing method.</response>
    [HttpPost("/api/meetups/sign-up/{id:guid}")]
    [Authorize(Roles = Roles.PlainUser)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MeetupInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUpForMeetup([FromRoute] Guid id)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.Users)
            .SingleOrDefaultAsync(meetup => meetup.Id == id);
        if(meetup is null)
        {
            return NotFound();
        }

        var user = await context.PlainUsers
            .Include(plainUser => plainUser.Meetups)
            .SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);
        if(user is null)
        {
            // We can't sign up non-existing user to meetup.
            return BadRequest();
        }

        var isAlreadySignUp = user.Meetups.Any(userMeetup => userMeetup.Id == meetup.Id);
        if(isAlreadySignUp)
        {
            // We can't twice sign up the user to meetup.
            return BadRequest();
        }

        user.Meetups.Add(meetup);
        await context.SaveChangesAsync();

        var meetupInfoDto = mapper.Map<MeetupInfoDto>(meetup);
        return Ok(meetupInfoDto);
    }
}
