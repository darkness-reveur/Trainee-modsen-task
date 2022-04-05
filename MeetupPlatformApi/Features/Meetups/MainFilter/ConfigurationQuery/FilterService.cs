namespace MeetupPlatformApi.Features.Meetups.MainFilter.ConfigurationQuery;

using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Features.Meetups.MainFilter.FilterSettings;

public class FilterService
{
    public IQueryable<Meetup> FilterQueryMeetups(
       IQueryable<Meetup> meetupsQuery,
       BaseFilterSettings meetupFilterSettings)
    {
        meetupsQuery = GetMeetupsFilteredByDate(meetupsQuery, meetupFilterSettings);

        meetupsQuery = GetMeetupsFilteredByLocation(meetupsQuery, meetupFilterSettings);

        meetupsQuery = GetMeetupsFilteredBySearchingString(meetupsQuery, meetupFilterSettings);

        return meetupsQuery;
    }

    private IQueryable<Meetup> GetMeetupsFilteredBySearchingString(
        IQueryable<Meetup> meetupsQuery,
        BaseFilterSettings meetupFilterSettings)
    {
        if (meetupFilterSettings.SearchString is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup =>
            meetup.Title.ToLower().Contains(meetupFilterSettings.SearchString.ToLower())
            || meetup.Description.ToLower().Contains(meetupFilterSettings.SearchString.ToLower()));
        }

        return meetupsQuery;
    }

    private IQueryable<Meetup> GetMeetupsFilteredByLocation(
        IQueryable<Meetup> meetupsQuery,
        BaseFilterSettings meetupFilterSettings)
    {
        if (meetupFilterSettings.Location is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.Location.ToLower().Contains(meetupFilterSettings.Location.ToLower()));
        }

        return meetupsQuery;
    }

    private IQueryable<Meetup> GetMeetupsFilteredByDate(
        IQueryable<Meetup> meetupsQuery,
        BaseFilterSettings meetupFilterSettings)
    {
        var date = meetupFilterSettings.StartTime?.Date;

        if (date is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.StartTime.Date == date);
        }

        return meetupsQuery;
    }
}

