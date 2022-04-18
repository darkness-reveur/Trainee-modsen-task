namespace MeetupPlatform.Api.Features.Meetups.RegisterNewMeetup;

using AutoMapper;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationDto, Meetup>();
        CreateMap<Meetup, RegisteredMeetupDto>();
    }
}
