using AutoMapper;
using MeetupPlatformApi.DataBase.Entities;
using MeetupPlatformApi.DataTransferObjects;

namespace MeetupPlatformApi.MeetupProfiles;

public class MeetupProfile : Profile
{
    public MeetupProfile()
    {
        CreateMap<MeetupEntity, MeetupOutputDto>();

        CreateMap<MeetupInputDto, MeetupEntity>();
    }
}
