namespace MeetupPlatform.Tests.Seedwork.TestCategories;

using System;
using Xunit.Sdk;

[TraitDiscoverer("MeetupPlatform.Tests.Seedwork.TestCategoryDiscoverer", "MeetupPlatform.Tests")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TestCategoryAttribute : Attribute, ITraitAttribute
{
    public TestCategory TestCategory { get; }

    public TestCategoryAttribute(TestCategory testCategory) =>
        TestCategory = testCategory;
}
