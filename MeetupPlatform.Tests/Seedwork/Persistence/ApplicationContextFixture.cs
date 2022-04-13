namespace MeetupPlatform.Tests.Seedwork.Persistence;

using MeetupPlatform.Api.Persistence.Configuration;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Tests.Seedwork.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ApplicationContextFixture
{
    private readonly DbContextOptions<ApplicationContext> dbContextOptions;

    public ApplicationContextFixture()
    {
        var testsConfiguration = new ConfigurationBuilder()
            .AddBaseAppSettingsJson()
            .AddLocalAppSettingsJson()
            .Build();
        var persistenceConfiguration = new PersistenceConfiguration(testsConfiguration);

        dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
            .UseNpgsql(persistenceConfiguration.ConnectionString)
            .Options;
    }

    public ApplicationContext CreateContext() =>
        new ApplicationContext(dbContextOptions);
}
