namespace MeetupPlatform.Api.Features.Authentication.RegisterNewUser;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationDto, User>();
        CreateMap<User, RegistrationResultDto.UserInfoDto>();
        CreateMap<TokenPair, TokenPairDto>();
    }
}
