using AutoMapper;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;

namespace MeetupPlatformApi.MappingProfiles;

public class MeetupMappingProfile : Profile
{
    public MeetupMappingProfile()
    {
        CreateMap<MeetupEntity, MeetupOutputDto>();
        CreateMap<MeetupInputDto, MeetupEntity>();
    }
}
