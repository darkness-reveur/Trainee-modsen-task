namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using AutoMapper;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Domain.Users;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationDto, Organizer>();
        CreateMap<Organizer, RegistrationResultDto.UserInfoDto>();
        CreateMap<RegistrationDto, PlainUser>();
        CreateMap<PlainUser, RegistrationResultDto.UserInfoDto>();
        CreateMap<TokenPair, TokenPairDto>();
    }
}
