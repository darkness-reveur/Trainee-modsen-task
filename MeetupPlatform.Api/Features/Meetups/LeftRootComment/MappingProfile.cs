namespace MeetupPlatform.Api.Features.Meetups.LeftRootComment;

using AutoMapper;
using MeetupPlatform.Api.Domain.Comments;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RootComment, CreatedCommentDto>();
    }
}
