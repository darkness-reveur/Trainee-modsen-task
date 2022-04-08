namespace MeetupPlatformApi.Features.Meetups.SignUpForMeetup;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Meetup, MeetupInfoDto>()
            .ForMember(meetup => meetup.UsersCount,
                options => options.MapFrom(meetup => meetup.SignedUpUsers.Count));
    }
}
