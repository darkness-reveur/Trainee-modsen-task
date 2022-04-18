namespace MeetupPlatformApi.Features.Meetups.AddContactToMeetup;

public class MeetupContactAddDto
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
