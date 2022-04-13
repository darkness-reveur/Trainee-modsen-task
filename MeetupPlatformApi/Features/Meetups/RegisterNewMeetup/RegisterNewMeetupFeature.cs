﻿namespace MeetupPlatformApi.Features.Meetups.RegisterNewMeetup;

using AutoMapper;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiSection(ApiSections.Meetups)]
public class RegisterNewMeetupFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public RegisterNewMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Register new meetup.
    /// </summary>
    /// <response code="201">Returns the newly created item.</response>
    [HttpPost("/api/meetups")]
    [Authorize(Roles = Roles.Organizer)]
    [ProducesResponseType(typeof(RegisteredMeetupDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterNewMeetup([FromBody] RegistrationDto registrationDto)
    {
        var meetup = mapper.Map<Meetup>(registrationDto);
        meetup.OrganizerId = CurrentUser.UserId;
        context.Meetups.Add(meetup);
        await context.SaveChangesAsync();

        var registeredMeetupDto = mapper.Map<RegisteredMeetupDto>(meetup);
        return Created(registeredMeetupDto);
    }
}
