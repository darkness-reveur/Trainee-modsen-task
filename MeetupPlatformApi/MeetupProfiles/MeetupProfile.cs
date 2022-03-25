using AutoMapper;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;

namespace MeetupPlatformApi.MeetupProfiles;

public class MeetupProfile : Profile
{
    public MeetupProfile()
    {
        CreateMap<MeetupEntity, MeetupOutputDto>();

        CreateMap<MeetupInputDto, MeetupEntity>();
    }
}

