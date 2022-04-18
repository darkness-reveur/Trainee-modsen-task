namespace MeetupPlatform.Api.Features.Meetups.Contacts.AddContact;

using AutoMapper;
using MeetupPlatform.Api.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AdditionContactDto, Contact>();
        CreateMap<Contact, AddedContactDto>();
    }
}
