namespace MeetupPlatformApi.Features.Contacts.UpdateContact;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Contacts)]
public class UpdateContactFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public UpdateContactFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Update contact by his id.
    /// </summary>
    /// <response code="204">If updating was successful.</response>
    /// <response code="404">If needed contact is null.</response>
    [HttpPut("/api/contacts/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateContact([FromRoute] Guid id, [FromBody]  UpdateContactDto updateContactDto)
    {
        var contact = await context.Contacts.SingleOrDefaultAsync(contact => contact.Id == id);
        if (contact is null)
        {
            return NotFound();
        }

        mapper.Map(updateContactDto, contact);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
