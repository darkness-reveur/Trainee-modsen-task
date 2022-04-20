namespace MeetupPlatform.Api.Features.Meetups.LeftReplyComment;

using AutoMapper;
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
    /// Left reply to root comment.
    /// </summary>
    /// <response code="404">If needed meetup is null or needed comment doesn't exist.</response>
    /// <response code="201">Returns the new created item.</response>
    [HttpPost("/api/meetups/{meetupId:guid}/comments/{commentId:guid}/replies")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreatedReplyDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> LeftReplyComment(
        [FromRoute] Guid meetupId, 
        [FromRoute] Guid commentId, 
        [FromBody] ReplyCreationDto creationReplyDto)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == meetupId);
        if(meetup is null)
        {
            return NotFound();
        }

        bool isCommentExistInMeetup = context.RootComments
            .Any(rootComment => rootComment.Id == commentId && rootComment.MeetupId == meetupId);
        if(!isCommentExistInMeetup)
        {
            return NotFound();
        }

        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == CurrentUser.UserId);
        if(user is null)
        {
            return Unauthorized();
        }

        var replyComment = new ReplyComment
        {
            Text = creationReplyDto.Text,
            AuthorId = user.Id,
            RootCommentId = commentId,
            PostedAt = DateTime.UtcNow
        };
        context.ReplyComments.Add(replyComment);
        await context.SaveChangesAsync();

        var createdReplyDto = mapper.Map<CreatedReplyDto>(replyComment);
        return Created(createdReplyDto);
    }
}
