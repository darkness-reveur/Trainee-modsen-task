namespace MeetupPlatformApi.Features.Contacts.GetContacts;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Contacts)]
public class GetContactsFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public GetContactsFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all contacts.
    /// </summary>
    /// <response code="200">Returns contacts items colection.</response>
    [HttpGet("/api/contacts")]
    [ProducesResponseType(typeof(IEnumerable<ContactInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await context.Contacts.ToListAsync();
        var contactsInfoDtos = mapper.Map<IEnumerable<ContactInfoDto>>(contacts);
        return Ok(contactsInfoDtos);
    }
}
