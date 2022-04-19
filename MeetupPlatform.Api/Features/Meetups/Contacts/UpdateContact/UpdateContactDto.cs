namespace MeetupPlatform.Api.Features.Meetups.Contacts.UpdateContact;

using System.ComponentModel.DataAnnotations;

public class UpdateContactDto
{
    /// <summary>
    /// Title of contact.
    /// </summary>
    /// <example>Phone number.</example>
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }

    /// <summary>
    /// Value of contact.
    /// </summary>
    /// <example>80(33)-374-21-24</example>
    [Required]
    [MaxLength(1000)]
    public string Value { get; set; }
}
