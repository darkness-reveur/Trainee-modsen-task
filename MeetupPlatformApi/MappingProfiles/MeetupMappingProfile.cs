namespace MeetupPlatformApi.MappingProfiles;

using AutoMapper;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;

public class MeetupMappingProfile : Profile
{
    public MeetupMappingProfile()
    {
        CreateMap<MeetupEntity, MeetupOutputDto>();
        CreateMap<MeetupInputDto, MeetupEntity>();
    }
}
