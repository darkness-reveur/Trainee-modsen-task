namespace MeetupPlatform.Api.Features.Authentication.RegisterNewUser;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain.Users;

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
