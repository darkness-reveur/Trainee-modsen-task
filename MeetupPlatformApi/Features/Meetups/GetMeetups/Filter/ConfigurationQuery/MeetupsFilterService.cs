namespace MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.ConfigurationQuery;

using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.FilterSettings;
using MeetupPlatformApi.Features.Meetups.MainFilter.ConfigurationQuery;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class MeetupsFilterService
{
    public async Task<List<Meetup>> GetMeetupsFilteredByFilterSettingsAsync(
        IQueryable<Meetup> meetupsQuery,
        MeetupsFilterSettings meetupFilterSettings)
    {
        FilterService filterService = new FilterService();

        meetupsQuery = filterService.FilterQueryMeetups(meetupsQuery, meetupFilterSettings);

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
}
