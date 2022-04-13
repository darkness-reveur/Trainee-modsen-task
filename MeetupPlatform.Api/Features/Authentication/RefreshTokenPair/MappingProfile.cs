namespace MeetupPlatform.Api.Features.Authentication.RefreshTokenPair;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TokenPair, TokenPairDto>();
    }
}
