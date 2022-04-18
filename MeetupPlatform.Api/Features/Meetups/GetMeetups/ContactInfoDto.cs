namespace MeetupPlatformApi.Features.Meetups.GetMeetup;

public class ContactInfoDto
{
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
