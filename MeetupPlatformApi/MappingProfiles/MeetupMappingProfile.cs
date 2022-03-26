using AutoMapper;
using MeetupPlatformApi.DataBase.Entities;
using MeetupPlatformApi.DataTransferObjects;

namespace MeetupPlatformApi.MappingProfiles;

public class MeetupMappingProfile : Profile
{
    public MeetupMappingProfile()
    {
        CreateMap<MeetupEntity, MeetupOutputDto>();
        CreateMap<MeetupInputDto, MeetupEntity>();
    }
}
