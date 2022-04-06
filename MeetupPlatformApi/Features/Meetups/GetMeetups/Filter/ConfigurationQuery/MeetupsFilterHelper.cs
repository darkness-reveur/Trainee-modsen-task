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

        switch (meetupFilterSettings.SortOptions)
        {
            case SortOptions.DescendingDateSort:

                meetupsQuery = meetupsQuery.OrderByDescending(meetups => meetups.StartTime);

                break;

            case SortOptions.AscendingDateSort:

                meetupsQuery = meetupsQuery.OrderBy(meetup => meetup.StartTime);

                break;
        }

        var skippedMeetups = (meetupFilterSettings.PageNumber - 1) * meetupFilterSettings.PageSize;

        return meetupsQuery
                .Skip(skippedMeetups)
                .Take(meetupFilterSettings.PageSize);
    }

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
        if (meetupFilterSettings.SearchString is null)
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
        if (meetupFilterSettings.Location is null)
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
        var startDate = meetupFilterSettings.StartTime is not null ? meetupFilterSettings.StartTime?.Date : null;

        var endDate = meetupFilterSettings.EndTime is not null ? meetupFilterSettings.EndTime?.Date.AddDays(1) : null;

        if (startDate is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.StartTime >= startDate);
        }

        if (endDate is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.EndTime < endDate);
        }

        return meetupsQuery;
    }
}
