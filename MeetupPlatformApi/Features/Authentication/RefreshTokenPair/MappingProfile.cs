namespace MeetupPlatformApi.Features.Authentication.RefreshTokenPair;

using AutoMapper;
using MeetupPlatformApi.Authentication.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TokenPair, TokenPairDto>();
    }
}
