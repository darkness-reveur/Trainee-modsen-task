namespace MeetupPlatformApi.MappingProfiles;

using AutoMapper;
using MeetupPlatformApi.Authentication;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;

public class AuthenticationMappingProfile : Profile
{
    public AuthenticationMappingProfile()
    {
        CreateMap<UserRegistrationDto, UserEntity>();
        CreateMap<UserEntity, UserOutputDto>();
        CreateMap<TokenPair, TokenPairDto>();
    }
}
