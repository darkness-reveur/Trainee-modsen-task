namespace MeetupPlatform.Api.Features.Meetups.LeftReplyComment;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain.Comments;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class LeftReplyCommentFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public LeftReplyCommentFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Left reply comment to root comment.
    /// </summary>
    /// <response code="404">If needed meetup is null or needed comment doesn't exist.</response>
    /// <response code="201">Returns the new created item.</response>
    [HttpPost("/api/meetups/{meetupId:guid}/comments/{commentId:guid}/replies")]
    [Authorize(Roles = Roles.PlainUser)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreatedReplyDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> LeftReplyComment([FromRoute] Guid meetupId, 
        [FromRoute] Guid commentId, 
        [FromBody] CreationReplyDto creationReplyDto)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.RootComments)
            .Include(meetup => meetup.SignedUpUsers)
            .Where(meetup => meetup.Id == meetupId)
            .SingleOrDefaultAsync();
        if(meetup is null)
        {
            return NotFound();
        }

        bool isCommentExistInMeetup = meetup.RootComments.Any(rootComment => rootComment.Id == commentId);
        if(!isCommentExistInMeetup)
        {
            return NotFound();
        }

        var user = await context.PlainUsers.Where(plainUser => plainUser.Id == CurrentUser.UserId).SingleOrDefaultAsync();
        if(user is null)
        {
            return Unauthorized();
        }

        bool isUserSignedUpForMeetup = meetup.SignedUpUsers.Any(plainUser => plainUser.Id == user.Id);
        if(!isUserSignedUpForMeetup)
        {
            // User can't to left reply to comment to meetup that hasn't had he as signed up.
            return Forbid();
        }

        var replyComment = new ReplyComment
        {
            Text = creationReplyDto.Text,
            PlainUserId = user.Id,
            RootCommentId = commentId
        };
        context.ReplyComments.Add(replyComment);
        await context.SaveChangesAsync();

        var createdReplyDto = mapper.Map<CreatedReplyDto>(replyComment);
        return Created(createdReplyDto);
    }
}
