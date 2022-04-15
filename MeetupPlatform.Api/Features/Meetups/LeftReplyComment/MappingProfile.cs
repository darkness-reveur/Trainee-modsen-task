namespace MeetupPlatform.Api.Features.Meetups.LeftReplyComment;

using AutoMapper;
using MeetupPlatform.Api.Domain.Comments;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ReplyComment, CreatedReplyDto>();
    }
}
