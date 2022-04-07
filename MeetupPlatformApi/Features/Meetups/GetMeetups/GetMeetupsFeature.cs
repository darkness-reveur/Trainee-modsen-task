﻿namespace MeetupPlatformApi.Features.Meetups.GetMeetups;

using AutoMapper;
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

    [HttpGet("/api/meetups")]
    public async Task<IActionResult> GetMeetups()
    {
        var meetups = await context.Meetups
            .Include(meetup => meetup.Users)
            .ToListAsync();
        var meetupInfoDtos = mapper.Map<IEnumerable<MeetupInfoDto>>(meetups);
        return Ok(meetupInfoDtos);
    }
}
