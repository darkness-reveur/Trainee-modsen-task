namespace MeetupPlatformApi.Features.Authentication.GetUser;

using AutoMapper;
using MeetupPlatformApi.Domain.Users;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserInfoDto>();
    }
}
