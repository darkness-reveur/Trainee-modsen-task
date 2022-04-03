namespace MeetupPlatformApi.MappingProfiles;

using AutoMapper;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Domain;

public class MeetupMappingProfile : Profile
{
    public MeetupMappingProfile()
    {
        CreateMap<Meetup, MeetupOutputDto>();
        CreateMap<MeetupInputDto, Meetup>();
    }
}
