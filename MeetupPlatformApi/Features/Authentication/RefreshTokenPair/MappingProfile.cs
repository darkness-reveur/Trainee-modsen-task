namespace MeetupPlatformApi.Features.Authentication.RefreshTokenPair;

using AutoMapper;
using MeetupPlatformApi.Authentication;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TokenPair, TokenPairDto>();
    }
}
