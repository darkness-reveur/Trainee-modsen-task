namespace MeetupPlatformApi.Features.Meetups.GetMeetups;

using AutoMapper;
using MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.ConfigurationQuery;
using MeetupPlatformApi.Features.Meetups.GetMeetups.Filter.FilterSettings;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    /// Get all meetups.
    /// </summary>
    /// <response code="200">Returns meetup items colection.</response>
    [HttpGet("/api/meetups")]
    [ProducesResponseType(typeof(MeetupInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMeetups([FromQuery] MeetupsFilterSettings filterSettings)
    {
        var meetupsQuery = MeetupsFilterHelper.GetMeetupsFilteredByFilterSettings(context.Meetups, filterSettings);
        var meetupInfoDtos = await mapper.ProjectTo<MeetupInfoDto>(meetupsQuery).ToListAsync();
        return Ok(meetupInfoDtos);
    }
}
