namespace MeetupPlatformApi.Features.Meetups.UpdateMeetup;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateMeetupDto, Meetup>();
    }
}
