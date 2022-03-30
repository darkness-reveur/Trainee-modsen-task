using AutoMapper;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;

namespace MeetupPlatformApi.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserForCreationDto, UserEntity>();
            CreateMap<UserEntity, UserOutputDto>();
        }
    }
}
