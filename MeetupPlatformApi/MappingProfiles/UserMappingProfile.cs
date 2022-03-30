namespace MeetupPlatformApi.MappingProfiles;

using AutoMapper;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserRegistrationDto, UserEntity>();
        CreateMap<UserEntity, UserOutputDto>();
    }
}
