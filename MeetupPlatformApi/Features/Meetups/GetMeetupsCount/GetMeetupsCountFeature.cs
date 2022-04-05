namespace MeetupPlatformApi.Features.Meetups.GetMeetupsCount;

using MeetupPlatformApi.Features.Meetups.GetMeetupsCount.Filter.ConfigurationQuery;
using MeetupPlatformApi.Features.Meetups.GetMeetupsCount.Filter.FilterSettings;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;

[ApiSection(ApiSections.Meetups)]
public class GetMeetupsCountFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public GetMeetupsCountFeature(ApplicationContext context)
    {
        this.context = context;
    }

    [HttpGet("/api/meetups/count")]
    public async Task<IActionResult> GetFilteredMeetupsCount([FromQuery] MeetupsCountFilterSettings filterSettings)
    {
        MeetupsCountFilterService meetupsFilterService = new MeetupsCountFilterService();

        var meetupsQuery = context.Meetups.AsQueryable();

        var meetupsCount = await meetupsFilterService.GetCountOfFilteredMeetupsAsync(meetupsQuery, filterSettings);

        return Ok(meetupsCount);
    }
}

