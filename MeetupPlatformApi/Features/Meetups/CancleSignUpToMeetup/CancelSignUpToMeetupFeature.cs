namespace MeetupPlatformApi.Features.Meetups.CancleSignUpToMeetup;

using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class CancelSignUpToMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public CancelSignUpToMeetupFeature(ApplicationContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Cancel sign up to meetup by its id.
    /// </summary>
    /// <response code="404">If needed meetup is null.</response>
    /// <response code="400">If user doesn't exist or the user is trying to sign up for meetup twice.</response>
    /// <response code="204">If there's not errors after executing method.</response>
    [HttpDelete("/api/meetups/cancel-sign-up/{id:guid}")]
    [Authorize(Roles = Roles.PlainUser)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelSignUpToMeetup([FromRoute] Guid id)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.SignedUpUsers)
            .SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }

        var user = await context.PlainUsers
            .Include(plainUser => plainUser.MeetupsSignedUpFor)
            .SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);
        if (user is null)
        {
            // We can't sign up non-existing user to meetup.
            return Unauthorized();
        }

        var isAlreadySignUp = user.MeetupsSignedUpFor.Any(userMeetup => userMeetup.Id == meetup.Id);
        if (!isAlreadySignUp)
        {
            // We can't delete not sign upped the user to meetup.
            return BadRequest();
        }

        meetup.SignedUpUsers.Remove(user);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
