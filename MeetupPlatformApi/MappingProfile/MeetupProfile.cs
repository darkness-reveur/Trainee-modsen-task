using AutoMapper;
using MeetupPlatformApi.Dto;
using MeetupPlatformApi.Entities;

namespace MeetupPlatformApi.AutoMapper
{
    public class MeetupProfile : Profile
    {
        public MeetupProfile()
        {
            CreateMap<MeetupOutputDto, MeetupEntity>();

            CreateMap<MeetupEntity, MeetupOutputDto>();

            CreateMap<MeetupInputDto, MeetupEntity>();

            CreateMap<MeetupEntity, MeetupInputDto>();
        }
    }
}
