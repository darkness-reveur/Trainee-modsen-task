namespace MeetupPlatform.Api.Features.Meetups.GetReplyComments.Filter.ConfigurationQuery;

using MeetupPlatform.Api.Domain.Comments;
using MeetupPlatform.Api.Features.Meetups.GetReplyComments.Filter.FilterSettings;
using System.Linq;

public static class ReplyCommentFilterHelper
{
    public static IQueryable<ReplyComment> GetReplyCommentsFilteredByFilterSettings(
        this IQueryable<ReplyComment> replyCommentsQuery,
        ReplyCommentFilterSettings replyCommentFilterSettings)
        => replyCommentsQuery
            .SortReplyCommentsByDescending()
            .GetPaginatedReplyCommentsBySettings(replyCommentFilterSettings);

    private static IQueryable<ReplyComment> SortReplyCommentsByDescending(this IQueryable<ReplyComment> replyCommentsQuery)
        => replyCommentsQuery.OrderByDescending(replyComment => replyComment.Posted);

    private static IQueryable<ReplyComment> GetPaginatedReplyCommentsBySettings(
        this IQueryable<ReplyComment> replyCommentsQuery,
        ReplyCommentFilterSettings replyCommentFilterSettings)
    {
        var skippedComments = (replyCommentFilterSettings.PageNumber - 1) * replyCommentFilterSettings.PageSize;
        return replyCommentsQuery
            .Skip(skippedComments)
            .Take(replyCommentFilterSettings.PageSize);
    }
}
