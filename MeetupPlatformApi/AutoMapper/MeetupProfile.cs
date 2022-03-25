using AutoMapper;
using MeetupPlatformApi.DTO;
using MeetupPlatformApi.Entities;

namespace MeetupPlatformApi.AutoMapper
{
    public class MeetupProfile : Profile
    {
        public MeetupProfile()
        {
            CreateMap<MeetupDTO, MeetupViewModel>();

            CreateMap<MeetupViewModel, MeetupDTO>();
        }
    }
}
