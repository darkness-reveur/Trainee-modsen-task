namespace MeetupPlatformApi.Features.Meetups.GetMeetups;

using AutoMapper;
using MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.ConfigurationQuery;
using MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.FilterSettings;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;

[ApiSection(ApiSections.Meetups)]
public class GetMeetupsFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetMeetupsFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet("/api/meetups")]
    public async Task<IActionResult> GetMeetups([FromQuery] MeetupsFilterSettings filterSettings)
    {
        if (filterSettings is null)
        {
            return BadRequest();
        }
        
        MeetupsFilterService meetupsFilterService = new MeetupsFilterService();

        var meetupsQuery = context.Meetups.AsQueryable();

        var meetupsList = await meetupsFilterService.GetMeetupsFilteredByFilterSettingsAsync(meetupsQuery, filterSettings);

        var meetupInfoDtos = mapper.Map<IEnumerable<MeetupInfoDto>>(meetupsList);

        if (meetupInfoDtos is null)
        {
            return NotFound();
        }

        return Ok(meetupInfoDtos);
    }
}
