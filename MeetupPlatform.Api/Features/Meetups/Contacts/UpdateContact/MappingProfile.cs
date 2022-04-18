namespace MeetupPlatform.Api.Features.Meetups.Contacts.UpdateContact;

using AutoMapper;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateContactDto, Contact>();
    }
}
