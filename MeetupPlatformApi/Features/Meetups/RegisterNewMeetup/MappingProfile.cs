namespace MeetupPlatformApi.Features.Meetups.RegisterNewMeetup;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationDto, Meetup>();
        CreateMap<Meetup, RegisteredMeetupDto>();
    }
}
