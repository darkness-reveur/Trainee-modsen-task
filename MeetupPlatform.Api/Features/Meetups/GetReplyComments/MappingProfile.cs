namespace MeetupPlatform.Api.Features.Meetups.GetReplyComments;

using AutoMapper;
using MeetupPlatform.Api.Domain.Comments;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ReplyComment, ReplyCommentInfoDto>();
    }
}
