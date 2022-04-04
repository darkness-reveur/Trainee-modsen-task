namespace MeetupPlatformApi.Features.Authentication.GetUser;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserInfoDto>();
    }
}
