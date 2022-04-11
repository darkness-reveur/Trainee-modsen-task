namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using AutoMapper;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Domain.Users;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, RegistrationResultDto.UserInfoDto>();
        CreateMap<RegistrationDto, Organizer>();
        CreateMap<RegistrationDto, PlainUser>();
        CreateMap<TokenPair, TokenPairDto>();
    }
}
