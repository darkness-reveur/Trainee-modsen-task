namespace MeetupPlatform.Api.Features.Meetups.GetRootComments;

using AutoMapper;
using MeetupPlatform.Api.Domain.Comments;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RootComment, RootCommentInfoDto>();
    }
}
