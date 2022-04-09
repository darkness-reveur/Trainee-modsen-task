namespace MeetupPlatform.Api.Features.Authentication.GetUser;

using AutoMapper;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserInfoDto>();
    }
}
