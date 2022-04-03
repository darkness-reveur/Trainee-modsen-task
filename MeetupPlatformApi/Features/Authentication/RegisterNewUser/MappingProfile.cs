namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationDto, User>();
        CreateMap<User, RegistrationResultDto.UserInfoDto>();
    }
}
