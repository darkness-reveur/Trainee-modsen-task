namespace MeetupPlatformApi.Features.Contacts.DeleteContact;

using AutoMapper;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Contacts)]
public class DeleteContactFeature : FeatureBase
{
    private readonly ApplicationContext context;

    public DeleteContactFeature(ApplicationContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Delete contact by id.
    /// </summary>
    /// <response code="204">If deleting was successful.</response>
    /// <response code="404">If needed contact is null.</response>
    [HttpDelete("/api/contacts/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
    {
        var contact = await context.Contacts.SingleOrDefaultAsync(contact => contact.Id == id);
        if (contact is null)
        {
            return NotFound();
        }

        context.Contacts.Remove(contact);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
