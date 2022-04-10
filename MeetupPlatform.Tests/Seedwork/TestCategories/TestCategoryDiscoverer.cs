namespace MeetupPlatform.Tests.Seedwork.TestCategories;

using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

public class TestCategoryDiscoverer : ITraitDiscoverer
{
    private const string TraitName = "Category";
    
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        var attributeInfo = traitAttribute as ReflectionAttributeInfo;
        if (attributeInfo?.Attribute is not TestCategoryAttribute testCategoryAttribute)
        {
            yield break;
        }
        
        var testCategoryName = testCategoryAttribute.TestCategory.ToString();
        yield return new KeyValuePair<string, string>(TraitName, testCategoryName);
    }
}
