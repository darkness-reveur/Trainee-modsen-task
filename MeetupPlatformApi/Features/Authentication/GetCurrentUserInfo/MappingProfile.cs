namespace MeetupPlatformApi.Features.Authentication.GetCurrentUserInfo;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserInfoDto>();
    }
}
