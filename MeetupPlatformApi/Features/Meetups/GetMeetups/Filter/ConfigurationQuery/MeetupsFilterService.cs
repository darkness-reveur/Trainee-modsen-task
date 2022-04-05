namespace MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.ConfigurationQuery;

using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.FilterSettings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class MeetupsFilterService
{
    public async Task<List<Meetup>> GetMeetupsFilteredByFilterSettingsAsync(
        IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        meetupsQuery = FilterQueryMeetups(meetupsQuery, meetupFilterSettings);

        meetupFilterSettings.PageNumber = meetupFilterSettings.PageNumber < 1 ?
            1 : meetupFilterSettings.PageNumber;

        meetupFilterSettings.PageSize = meetupFilterSettings.PageSize < 1 ?
            10 : meetupFilterSettings.PageSize;

        var skipedMeetups = (meetupFilterSettings.PageNumber - 1) * meetupFilterSettings.PageSize;

        if (meetupFilterSettings.IsDescendingDateSort)
        {
            meetupsQuery = meetupsQuery.OrderByDescending(meetups => meetups.StartTime);
        }
        else
        {
            meetupsQuery = meetupsQuery.OrderBy(meetup => meetup.StartTime);
        }

        return await meetupsQuery
                .Skip(skipedMeetups)
                .Take(meetupFilterSettings.PageSize)
                .ToListAsync(); ;
    }

    public IQueryable<Meetup> FilterQueryMeetups(
       IQueryable<Meetup> meetupsQuery,
       MeetupsFilterSettings meetupFilterSettings)
    {
        meetupsQuery = GetMeetupsFilteredByDate(meetupsQuery, meetupFilterSettings);

        meetupsQuery = GetMeetupsFilteredByLocation(meetupsQuery, meetupFilterSettings);

        meetupsQuery = GetMeetupsFilteredBySearchingString(meetupsQuery, meetupFilterSettings);

        return meetupsQuery;
    }

    private IQueryable<Meetup> GetMeetupsFilteredBySearchingString(
        IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
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
        MeetupsFilterSettings meetupFilterSettings)
    {
        if (meetupFilterSettings.Location is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.Location.ToLower().Contains(meetupFilterSettings.Location.ToLower()));
        }

        return meetupsQuery;
    }

    private IQueryable<Meetup> GetMeetupsFilteredByDate(
        IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        var date = meetupFilterSettings.StartTime?.Date;

        if (date is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.StartTime.Date == date);
        }

        return meetupsQuery;
    }
}
