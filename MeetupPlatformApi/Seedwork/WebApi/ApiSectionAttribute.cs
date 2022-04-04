namespace MeetupPlatformApi.Seedwork.WebApi;

using Microsoft.AspNetCore.Http.Metadata;

[AttributeUsage(AttributeTargets.Class)]
public class ApiSectionAttribute : Attribute, ITagsMetadata
{
    public IReadOnlyList<string> Tags { get; }

    public ApiSectionAttribute(ApiSections apiSection)
    {
        var apiSectionName = apiSection.ToString();
        Tags = new[] {apiSectionName};
    }
}
