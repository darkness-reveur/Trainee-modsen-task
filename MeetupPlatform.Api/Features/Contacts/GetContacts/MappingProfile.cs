namespace MeetupPlatformApi.Features.Contacts.GetContacts;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, ContactInfoDto>();
    }
}
