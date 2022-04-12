namespace MeetupPlatformApi.Features.Contacts.GetContacts;

using System.ComponentModel.DataAnnotations;

public class ContactInfoDto
{
    /// <summary>
    /// Contact id value
    /// </summary>
    /// <example>cc3e5bce-a8c0-4258-b663-71ec8f2b6446</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Contact description
    /// </summary>
    /// <example>Telegram</example>
    public string Title { get; set; }

    /// <summary>
    /// Contact value
    /// </summary>
    /// <example>t.me/Avarage_User</example>
    public string Value { get; set; }
}
