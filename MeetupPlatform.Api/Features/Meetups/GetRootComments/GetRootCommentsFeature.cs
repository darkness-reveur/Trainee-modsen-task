namespace MeetupPlatform.Api.Features.Meetups.GetRootComments;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Features.Meetups.GetRootComments.Filter.FilterSettings;
using MeetupPlatform.Api.Features.Meetups.GetRootComments.Filter.ConfigurationQuery;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class GetRootCommentsFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetRootCommentsFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all comments of meetup.
    /// </summary>
    /// <response code="404">If needed meetup is null.</response>
    /// <response code="200">Returns meetup's list of comments.</response>
    [HttpGet("/api/meetups/{id:guid}/comments")]
    [Authorize(Roles = Roles.PlainUser)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RootCommentInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRootComments([FromRoute] Guid id, 
        [FromQuery] RootCommentFilterSettings filterSettings)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.SignedUpUsers)
            .Where(meetup => meetup.Id == id)
            .SingleOrDefaultAsync();
        if(meetup is null)
        {
            return NotFound();
        }

        bool isUserSignedUpForMeetup = meetup.SignedUpUsers.Any(plainUser => plainUser.Id == CurrentUser.UserId);
        if(!isUserSignedUpForMeetup)
        {
            return Forbid();
        }

        var rootComments = await context.RootComments
            .GetRootCommentsFilteredByFilterSettings(filterSettings)
            .ToListAsync();
        var rootCommentInfoDtos = mapper.Map<List<RootCommentInfoDto>>(rootComments);
        return Ok(rootCommentInfoDtos);
    }
}
