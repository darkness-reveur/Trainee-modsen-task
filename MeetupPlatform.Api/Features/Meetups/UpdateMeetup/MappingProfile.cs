namespace MeetupPlatform.Api.Features.Meetups.UpdateMeetup;

using AutoMapper;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateMeetupDto, Meetup>();
    }
}
