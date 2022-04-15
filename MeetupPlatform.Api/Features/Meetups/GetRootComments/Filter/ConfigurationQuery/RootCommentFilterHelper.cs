namespace MeetupPlatform.Api.Features.Meetups.GetRootComments.Filter.ConfigurationQuery;

using MeetupPlatform.Api.Domain.Comments;
using MeetupPlatform.Api.Features.Meetups.GetRootComments.Filter.FilterSettings;
using System.Linq;

public static class RootCommentFilterHelper
{
    public static IQueryable<RootComment> GetRootCommentsFilteredByFilterSettings(
        this IQueryable<RootComment> rootCommentsQuery,
        RootCommentFilterSettings rootCommentFilterSettings) 
        => rootCommentsQuery
            .SortRootCommentsByDescending()
            .GetPaginatedRootCommentsBySettings(rootCommentFilterSettings);

    private static IQueryable<RootComment> SortRootCommentsByDescending(this IQueryable<RootComment> rootCommentsQuery)
        => rootCommentsQuery.OrderByDescending(rootComment => rootComment.Posted);

    private static IQueryable<RootComment> GetPaginatedRootCommentsBySettings(
        this IQueryable<RootComment> rootCommentsQuery,
        RootCommentFilterSettings rootCommentFilterSettings)
    {
        var skippedComments = (rootCommentFilterSettings.PageNumber - 1) * rootCommentFilterSettings.PageSize;
        return rootCommentsQuery
            .Skip(skippedComments)
            .Take(rootCommentFilterSettings.PageSize);
    }
}
