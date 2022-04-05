namespace MeetupPlatformApi.Filter.ConfigurationQuery;

using MeetupPlatformApi.Entities;
using MeetupPlatformApi.Filter.FilterSettings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class MeetupsFilterService
{
    public async Task<int> GetCountOfFilteredMeetupsAsync(
        IQueryable<MeetupEntity> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        return await FilterQueryMeetups(meetupsQuery, meetupFilterSettings).CountAsync();
    }

    public async Task<List<MeetupEntity>> GetMeetupsFilteredByFilterSettingsAsync(
        IQueryable<MeetupEntity> meetupsQuery,
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

    public IQueryable<MeetupEntity> FilterQueryMeetups(
        IQueryable<MeetupEntity> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        meetupsQuery = GetMeetupsFilteredByDate(meetupsQuery, meetupFilterSettings);

        meetupsQuery = GetMeetupsFilteredByLocation(meetupsQuery, meetupFilterSettings);

        meetupsQuery = GetMeetupsFilteredBySearchingString(meetupsQuery, meetupFilterSettings);

        return meetupsQuery;
    }

    public IQueryable<MeetupEntity> GetMeetupsFilteredBySearchingString(
        IQueryable<MeetupEntity> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        if (meetupFilterSettings.SearchString is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup =>
            meetup.Name.ToLower().Contains(meetupFilterSettings.SearchString.ToLower())
            || meetup.Description.ToLower().Contains(meetupFilterSettings.SearchString.ToLower()));
        }

        return meetupsQuery;
    }

    public IQueryable<MeetupEntity> GetMeetupsFilteredByLocation(
        IQueryable<MeetupEntity> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        if (meetupFilterSettings.Location is not null)
        {
            meetupsQuery = meetupsQuery.Where(meetup => meetup.Location.ToLower().Contains(meetupFilterSettings.Location.ToLower()));
        }

        return meetupsQuery;
    }

    public IQueryable<MeetupEntity> GetMeetupsFilteredByDate(
        IQueryable<MeetupEntity> meetupsQuery,
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
