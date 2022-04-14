namespace MeetupPlatform.Api.Features.Meetups.GetMeetups.Filter.ConfigurationQuery;

using MeetupPlatform.Api.Domain;
using MeetupPlatform.Api.Features.Meetups.GetMeetups.Filter.FilterSettings;
using System.Linq;

public static class MeetupsFilterHelper
{
    public static IQueryable<Meetup> GetMeetupsFilteredByFilterSettings(
        this IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        meetupsQuery = meetupsQuery.Filter(meetupFilterSettings);
        meetupsQuery = meetupsQuery.SortSelection(meetupFilterSettings.SortOption);
        return meetupsQuery.GetDataByPaginationSettings(meetupFilterSettings);
    }

    private static IQueryable<Meetup> GetDataByPaginationSettings(
        this IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        var skippedMeetups = (meetupFilterSettings.PageNumber - 1) * meetupFilterSettings.PageSize;
        return meetupsQuery
                .Skip(skippedMeetups)
                .Take(meetupFilterSettings.PageSize);
    }

    private static IQueryable<Meetup> SortSelection(this IQueryable<Meetup> meetupsQuery, SortOptions sortOptions) => sortOptions switch
    {
        SortOptions.DescendingStartTime          => meetupsQuery.OrderByDescending(meetups => meetups.StartTime),
        SortOptions.DescendingSignedUpUsersCount => meetupsQuery.OrderByDescending(meetups => meetups.SignedUpUsers.Count),
        SortOptions.AscendingStartTime           => meetupsQuery.OrderBy(meetups => meetups.StartTime),
        SortOptions.AscendingSignedUpUsersCount  => meetupsQuery.OrderBy(meetups => meetups.SignedUpUsers.Count),
        _ => throw new ArgumentOutOfRangeException(nameof(sortOptions), $"Not expected value: {sortOptions}")
    };

    private static IQueryable<Meetup> Filter(
       this IQueryable<Meetup> meetupsQuery,
       MeetupsFilterSettings meetupFilterSettings)
    {
        meetupsQuery = meetupsQuery.FilterByDate(meetupFilterSettings);
        meetupsQuery = meetupsQuery.FilterByLocation(meetupFilterSettings);
        meetupsQuery = meetupsQuery.FilterBySearchString(meetupFilterSettings);
        return meetupsQuery;
    }

    private static IQueryable<Meetup> FilterBySearchString(
        this IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        if (string.IsNullOrEmpty(meetupFilterSettings.SearchString))
        {
            return meetupsQuery;
        }

        meetupsQuery = meetupsQuery.Where(meetup =>
            meetup.Title.ToLower().Contains(meetupFilterSettings.SearchString.ToLower())
            || meetup.Description.ToLower().Contains(meetupFilterSettings.SearchString.ToLower()));
        return meetupsQuery;
    }

    private static IQueryable<Meetup> FilterByLocation(
        this IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        if (string.IsNullOrEmpty(meetupFilterSettings.Location))
        {
            return meetupsQuery;
        }

        meetupsQuery = meetupsQuery.Where(meetup => meetup.Location.ToLower().Contains(meetupFilterSettings.Location.ToLower()));
        return meetupsQuery;
    }

    private static IQueryable<Meetup> FilterByDate(
        this IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        if (meetupFilterSettings.BottomBoundOfStartTime is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.StartTime >= meetupFilterSettings.BottomBoundOfStartTime);
        }

        if (meetupFilterSettings.UpperBoundOfStartTime is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.StartTime <= meetupFilterSettings.UpperBoundOfStartTime);
        }

        return meetupsQuery;
    }
}
