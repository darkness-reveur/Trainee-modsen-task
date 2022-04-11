﻿namespace MeetupPlatformApi.Features.Meetups.GetMeetup;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Meetup, MeetupInfoDto>()
            .ForMember(meetup => meetup.SignedUpUsersCount, 
                options => options.MapFrom(meetup => meetup.SignedUpUsers.Count));
    }
}
