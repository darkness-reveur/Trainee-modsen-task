namespace MeetupPlatform.Api.Features.Authentication.GetCurrentUserInfo;

using AutoMapper;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserInfoDto>();
    }
}
