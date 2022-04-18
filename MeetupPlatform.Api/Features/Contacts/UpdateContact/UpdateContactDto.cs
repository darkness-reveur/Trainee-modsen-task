namespace MeetupPlatformApi.Features.Contacts.UpdateContact;

using System.ComponentModel.DataAnnotations;

public class UpdateContactDto
{
    /// <summary>
    /// Contact description
    /// </summary>
    /// <example>Telegram</example>
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    /// <summary>
    /// Contact value
    /// </summary>
    /// <example>t.me/Avarage_User</example>
    [Required]
    public string Value { get; set; }
}
