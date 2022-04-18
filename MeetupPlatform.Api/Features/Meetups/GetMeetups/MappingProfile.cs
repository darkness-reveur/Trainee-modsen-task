﻿namespace MeetupPlatform.Api.Features.Meetups.GetMeetups;

using AutoMapper;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Meetup, MeetupInfoDto>();
        CreateMap<Contact,ContactInfoDto>();
        CreateMap<Meetup, MeetupInfoDto>()
            .ForMember(meetup => meetup.SignedUpUsersCount, 
                options => options.MapFrom(meetup => meetup.SignedUpUsers.Count));
    }
}
