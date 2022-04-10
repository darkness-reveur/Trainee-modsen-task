namespace MeetupPlatform.Tests.MigrationTesting;

using System.Threading.Tasks;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Tests.Seedwork.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class MigrationTest : IClassFixture<ApplicationContextFixture>
{
    private readonly ApplicationContext context;

    public MigrationTest(ApplicationContextFixture applicationContextFixture) =>
        context = applicationContextFixture.CreateContext();

    [Fact]
    public async Task MigrateUp_Success()
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }
}
