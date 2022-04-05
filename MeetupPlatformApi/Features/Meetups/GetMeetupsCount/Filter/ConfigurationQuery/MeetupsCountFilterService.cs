namespace MeetupPlatformApi.Features.Meetups.GetMeetupsCount.Filter.ConfigurationQuery;

using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Features.Meetups.GetMeetupsCount.Filter.FilterSettings;
using MeetupPlatformApi.Features.Meetups.MainFilter.ConfigurationQuery;
using Microsoft.EntityFrameworkCore;

public class MeetupsCountFilterService
{
    public async Task<int> GetCountOfFilteredMeetupsAsync(
       IQueryable<Meetup> meetupsQuery,
       MeetupsCountFilterSettings meetupFilterSettings)
    {
        FilterService filterService = new FilterService();

        return await filterService.FilterQueryMeetups(meetupsQuery, meetupFilterSettings).CountAsync();
    }
}
