namespace MeetupPlatformApi.Features.Contacts.UpdateContact;

using AutoMapper;
using MeetupPlatformApi.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateContactDto, Contact>();
    }
}
