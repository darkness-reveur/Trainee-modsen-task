namespace MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.ConfigurationQuery;

using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.FilterSettings;
using System.Linq;

public static class MeetupsFilterHelper
{
    public static IQueryable<Meetup> GetMeetupsFilteredByFilterSettings(
        IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        meetupsQuery = meetupsQuery.Filter(meetupFilterSettings);
        meetupsQuery = SortSelection(meetupsQuery, meetupFilterSettings.SortOption);
        var skippedMeetups = (meetupFilterSettings.PageNumber - 1) * meetupFilterSettings.PageSize;

        return meetupsQuery
                .Skip(skippedMeetups)
                .Take(meetupFilterSettings.PageSize);
    }

    private static IQueryable<Meetup> SortSelection(IQueryable<Meetup> meetupsQuery, SortOptions sortOptions) => sortOptions switch
    {
        SortOptions.DescendingStartTime => meetupsQuery.OrderByDescending(meetups => meetups.StartTime),
        SortOptions.AscendingStartTime  => meetupsQuery.OrderBy(meetup => meetup.StartTime),
        _ => throw new ArgumentOutOfRangeException(nameof(sortOptions), $"Not expected direction value: {sortOptions}")
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
