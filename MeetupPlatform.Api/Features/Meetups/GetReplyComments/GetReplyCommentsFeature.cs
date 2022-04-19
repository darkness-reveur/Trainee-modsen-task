﻿namespace MeetupPlatform.Api.Features.Meetups.GetReplyComments;

using AutoMapper;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class GetReplyCommentsFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetReplyCommentsFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all replies of comment.
    /// </summary>
    /// <response code="404">If needed meetup or comment is null.</response>
    /// <response code="200">Returns root comment's list of replies.</response>
    [HttpGet("/api/meetups/{meetupId:guid}/comments/{commentId:guid}/replies")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ReplyCommentInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReplyComments([FromRoute] Guid meetupId, [FromRoute] Guid commentId)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.SignedUpUsers)
            .Include(meetup => meetup.Comments)
            .Where(meetup => meetup.Id == meetupId)
            .SingleOrDefaultAsync();
        if(meetup is null)
        {
            return NotFound();
        }

        bool isUserSignedUpForMeetup = meetup.SignedUpUsers.Any(plainUser => plainUser.Id == CurrentUser.UserId);
        if (!isUserSignedUpForMeetup)
        {
            return Forbid();
        }

        var rootComment = meetup.Comments
            .Where(rootComment => rootComment.Id == commentId)
            .SingleOrDefault();
        if(rootComment is null)
        {
            return NotFound();
        }

        var replyComments = await context.ReplyComments
            .Where(replyComment => replyComment.RootCommentId == commentId)
            .ToListAsync();
        var replyCommentInfoDtos = mapper.Map<List<ReplyCommentInfoDto>>(replyComments);
        return Ok(replyCommentInfoDtos);
    }
}
