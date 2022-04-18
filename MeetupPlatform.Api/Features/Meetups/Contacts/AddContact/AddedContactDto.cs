namespace MeetupPlatform.Api.Features.Meetups.Contacts.AddContact;

using System.ComponentModel.DataAnnotations;

public class AddedContactDto
{
    /// <summary>
    /// Contact id value.
    /// </summary>
    /// <example>cc3e5bce-a8c0-4258-b663-71ec8f2b6446</example>
    [Required]
    public Guid Id { get; set; }

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
