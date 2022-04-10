namespace MeetupPlatform.Tests.MigrationTesting;

using System;
using System.Threading.Tasks;
using Dapper;
using MeetupPlatform.Tests.Seedwork.Persistence;
using Xunit;

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
    // ReSharper disable once InconsistentNaming
    public async Task MeetupPKFromIntToGuid()
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
            Name = "Meetup #1",
            Description = "Meetup #1 Description",
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
}
