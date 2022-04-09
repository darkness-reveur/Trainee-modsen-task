﻿namespace MeetupPlatform.Api.Features.Meetups.GetMeetup;

using AutoMapper;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Meetup, MeetupInfoDto>();
    }
}
