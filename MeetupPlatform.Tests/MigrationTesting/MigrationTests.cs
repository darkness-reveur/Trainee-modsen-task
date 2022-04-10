namespace MeetupPlatform.Tests.MigrationTesting;

using System;
using System.Threading.Tasks;
using Dapper;
using MeetupPlatform.Tests.Seedwork.Persistence;
using MeetupPlatform.Tests.Seedwork.TestCategories;
using Xunit;

[TestCategory(TestCategory.MigrationTest)]
public class MigrationTests : IClassFixture<ApplicationContextFixture>
{
    private readonly DbMigrator dbMigrator;

    public MigrationTests(ApplicationContextFixture applicationContextFixture) =>
        dbMigrator = new DbMigrator(applicationContextFixture.CreateContext());

    [Fact]
    public async Task AllMigrationsOnEmptyDb()
    {
        await dbMigrator.InitializeDb();
        await dbMigrator.MigrateAllWayUp();
        await dbMigrator.MigrateAllWayDown();
    }

    [Fact]
    public async Task AddFluentApiMigrationTest()
    {
        // Common
        const string migrationUnderTest = "AddFluentApi";
        var meetupSample = new
        {
            Id = 1,
            Name = "Meetup",
            Description = "Meetup's Description",
            StartTime = new DateTime(2022, 04, 10, 10, 23, 54).ToUniversalTime(),
            EndTime = new DateTime(2022, 04, 10, 10, 23, 55).ToUniversalTime(),
        };
        var userSample = new
        {
            Id = Guid.NewGuid(),
            Username = "User",
            Password = "User's Password",
        };
        
        // Setup 
        await dbMigrator.InitializeDb();
        await dbMigrator.MigrateBefore(migrationUnderTest);
        const string insertMeetupSql = @"
            INSERT INTO ""Meetups""(""Id"", ""Name"", ""Description"", ""StartTime"", ""EndTime"")
            VALUES (@Id, @Name, @Description, @StartTime, @EndTime)";
        await dbMigrator.Connection.ExecuteAsync(insertMeetupSql, meetupSample);
        const string insertUserSql = @"
            INSERT INTO ""Users""(""Id"", ""Username"", ""Password"")
            VALUES (@Id, @Username, @Password)";
        await dbMigrator.Connection.ExecuteAsync(insertUserSql, userSample);
        
        // Check if the seed data is preserved after migration is applied
        await dbMigrator.MigrateOneStepUp();
        const string selectMeetupAfterMigrationSql = @"
            SELECT id AS ""Id"", name AS ""Name"", description AS ""Description"", start_time AS ""StartTime"", end_time AS ""EndTime""
            FROM meetups";
        var meetupAfterMigration = await dbMigrator.Connection.QuerySingleAsync(selectMeetupAfterMigrationSql);
        Assert.Equal(meetupSample.Id, (int) meetupAfterMigration.Id);
        Assert.Equal(meetupSample.Name, (string) meetupAfterMigration.Name);
        Assert.Equal(meetupSample.Description, (string) meetupAfterMigration.Description);
        Assert.Equal(meetupSample.StartTime, (DateTime) meetupAfterMigration.StartTime);
        Assert.Equal(meetupSample.EndTime, (DateTime) meetupAfterMigration.EndTime);
        const string selectUserAfterMigrationSql = @"
            SELECT id AS ""Id"", username AS ""Username"", password AS ""Password""
            FROM users";
        var userAfterMigration = await dbMigrator.Connection.QuerySingleAsync(selectUserAfterMigrationSql);
        Assert.Equal(userSample.Id, (Guid) userAfterMigration.Id);
        Assert.Equal(userSample.Username, (string) userAfterMigration.Username);
        Assert.Equal(userSample.Password, (string) userAfterMigration.Password);
        
        // Check if the seed data is preserved after migration is rolled back
        await dbMigrator.MigrateOneStepDown();
        const string selectMeetupAfterMigrationRollbackSql = @"
            SELECT ""Id"", ""Name"", ""Description"", ""StartTime"", ""EndTime""
            FROM ""Meetups""";
        var meetupAfterMigrationRollback = await dbMigrator.Connection.QuerySingleAsync(selectMeetupAfterMigrationRollbackSql);
        Assert.Equal(meetupSample.Id, (int) meetupAfterMigrationRollback.Id);
        Assert.Equal(meetupSample.Name, (string) meetupAfterMigrationRollback.Name);
        Assert.Equal(meetupSample.Description, (string) meetupAfterMigrationRollback.Description);
        Assert.Equal(meetupSample.StartTime, (DateTime) meetupAfterMigrationRollback.StartTime);
        Assert.Equal(meetupSample.EndTime, (DateTime) meetupAfterMigrationRollback.EndTime);
        const string selectUserAfterMigrationRollbackSql = @"
            SELECT ""Id"", ""Username"", ""Password""
            FROM ""Users""";
        var userAfterMigrationRollback = await dbMigrator.Connection.QuerySingleAsync(selectUserAfterMigrationRollbackSql);
        Assert.Equal(userSample.Id, (Guid) userAfterMigrationRollback.Id);
        Assert.Equal(userSample.Username, (string) userAfterMigrationRollback.Username);
        Assert.Equal(userSample.Password, (string) userAfterMigrationRollback.Password);
    }

    [Fact]
    // ReSharper disable once InconsistentNaming
    public async Task MeetupPKFromIntToGuidMigrationTest()
    {
        // Common
        const string migrationUnderTest = "MeetupPKFromIntToGuid";
        const string insertMeetupSql = @"
            INSERT INTO meetups(name, description, start_time, end_time)
            VALUES (@Name, @Description, @StartTime, @EndTime)";
        const string selectMeetupSql = @"
            SELECT name AS ""Name"", description AS ""Description"", start_time AS ""StartTime"", end_time AS ""EndTime""
            FROM meetups";
        var meetupSample = new
        {
            Name = "Meetup",
            Description = "Meetup's Description",
            StartTime = new DateTime(2022, 04, 10, 10, 23, 54).ToUniversalTime(),
            EndTime = new DateTime(2022, 04, 10, 10, 23, 55).ToUniversalTime(),
        };
        
        // Setup 
        await dbMigrator.InitializeDb();
        await dbMigrator.MigrateBefore(migrationUnderTest);
        await dbMigrator.Connection.ExecuteAsync(insertMeetupSql, meetupSample);
        
        // Check if the seed data is preserved after migration is applied
        await dbMigrator.MigrateOneStepUp();
        var dataAfterMigration = await dbMigrator.Connection.QuerySingleAsync(selectMeetupSql);
        Assert.Equal(meetupSample.Name, (string) dataAfterMigration.Name);
        Assert.Equal(meetupSample.Description, (string) dataAfterMigration.Description);
        Assert.Equal(meetupSample.StartTime, (DateTime) dataAfterMigration.StartTime);
        Assert.Equal(meetupSample.EndTime, (DateTime) dataAfterMigration.EndTime);
        
        // Check if the seed data is preserved after migration is rolled back
        await dbMigrator.MigrateOneStepDown();
        var dataAfterMigrationRollback = await dbMigrator.Connection.QuerySingleAsync(selectMeetupSql);
        Assert.Equal(meetupSample.Name, (string) dataAfterMigrationRollback.Name);
        Assert.Equal(meetupSample.Description, (string) dataAfterMigrationRollback.Description);
        Assert.Equal(meetupSample.StartTime, (DateTime) dataAfterMigrationRollback.StartTime);
        Assert.Equal(meetupSample.EndTime, (DateTime) dataAfterMigrationRollback.EndTime);
    }

    [Fact]
    // Note: Migration name is wrong - the desired name for field is "Title".
    public async Task RenameMeetupNameToTopicMigrationTest()
    {
        // Common
        const string migrationUnderTest = "RenameMeetupNameToTopic";
        var meetupSample = new
        {
            Name = "Meetup",
            Description = "Meetup's Description",
            StartTime = new DateTime(2022, 04, 10, 10, 23, 54).ToUniversalTime(),
            EndTime = new DateTime(2022, 04, 10, 10, 23, 55).ToUniversalTime(),
        };
        
        // Setup 
        await dbMigrator.InitializeDb();
        await dbMigrator.MigrateBefore(migrationUnderTest);
        const string insertMeetupSql = @"
            INSERT INTO meetups(name, description, start_time, end_time)
            VALUES (@Name, @Description, @StartTime, @EndTime)";
        await dbMigrator.Connection.ExecuteAsync(insertMeetupSql, meetupSample);
        
        // Check if the seed data is preserved after migration is applied
        await dbMigrator.MigrateOneStepUp();
        const string selectMeetupAfterMigrationSql = @"
            SELECT title AS ""Title"", description AS ""Description"", start_time AS ""StartTime"", end_time AS ""EndTime""
            FROM meetups";
        var meetupAfterMigration = await dbMigrator.Connection.QuerySingleAsync(selectMeetupAfterMigrationSql);
        Assert.Equal(meetupSample.Name, (string) meetupAfterMigration.Title);
        Assert.Equal(meetupSample.Description, (string) meetupAfterMigration.Description);
        Assert.Equal(meetupSample.StartTime, (DateTime) meetupAfterMigration.StartTime);
        Assert.Equal(meetupSample.EndTime, (DateTime) meetupAfterMigration.EndTime);
        
        // Check if the seed data is preserved after migration is rolled back
        await dbMigrator.MigrateOneStepDown();
        const string selectMeetupAfterMigrationRollbackSql = @"
            SELECT name AS ""Name"", description AS ""Description"", start_time AS ""StartTime"", end_time AS ""EndTime""
            FROM meetups";
        var meetupAfterMigrationRollback = await dbMigrator.Connection.QuerySingleAsync(selectMeetupAfterMigrationRollbackSql);
        Assert.Equal(meetupSample.Name, (string) meetupAfterMigrationRollback.Name);
        Assert.Equal(meetupSample.Description, (string) meetupAfterMigrationRollback.Description);
        Assert.Equal(meetupSample.StartTime, (DateTime) meetupAfterMigrationRollback.StartTime);
        Assert.Equal(meetupSample.EndTime, (DateTime) meetupAfterMigrationRollback.EndTime);
    }
}
