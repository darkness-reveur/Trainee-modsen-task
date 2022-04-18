namespace MeetupPlatformApi.Features.Meetups.AddContactToMeetup;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MeetupContactAddDto, Contact>();
    }
}
