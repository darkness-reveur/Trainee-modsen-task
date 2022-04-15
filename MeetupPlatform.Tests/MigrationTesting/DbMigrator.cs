namespace MeetupPlatform.Tests.MigrationTesting;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

public class DbMigrator
{
    public IDbConnection Connection => database.GetDbConnection();

    private readonly DatabaseFacade database;
    private readonly IMigrator migrator;

    public DbMigrator(DbContext context)
    {
        database = context.Database;
        migrator = context.GetService<IMigrator>();
    }

    public async Task InitializeDb()
    {
        await database.EnsureDeletedAsync();
        await migrator.MigrateAsync(Migration.InitialDatabase);
    }

    public async Task MigrateOneStepUp()
    {
        var pendingMigrations = await database.GetPendingMigrationsAsync();
        var nextMigration = pendingMigrations.First();
        await migrator.MigrateAsync(nextMigration);
    }

    public async Task MigrateAllWayUp()
    {
        await migrator.MigrateAsync();
    }

    public async Task MigrateOneStepDown()
    {
        var appliedMigrations = await database.GetAppliedMigrationsAsync();
        var previousMigration = appliedMigrations.Reverse().Skip(1).First();
        await migrator.MigrateAsync(previousMigration);
    }

    public async Task MigrateAllWayDown()
    {
        await migrator.MigrateAsync(Migration.InitialDatabase);
    }

    public async Task MigrateBefore(string shortMigrationName)
    {
        var shortMigrationNamesMapping = GetShortMigrationNamesMapping();
        var fullMigrationName = shortMigrationNamesMapping[shortMigrationName];
        
        var migrations = database.GetMigrations().ToArray();
        var migrationIndex = Array.IndexOf(migrations, fullMigrationName);
        var targetMigration = migrations[migrationIndex - 1];
        
        await migrator.MigrateAsync(targetMigration);
    }

    private IDictionary<string, string> GetShortMigrationNamesMapping()
    {
        var shortMigrationNames = new Dictionary<string, string>();
        
        var fullMigrationNames = database.GetMigrations();
        foreach (var fullMigrationName in fullMigrationNames)
        {
            const string migrationNamePattern = @"^(?<TimeStamp>\d+)_(?<MigrationName>.+)$";
            var match = Regex.Match(fullMigrationName, migrationNamePattern);
            if (!match.Success)
            {
                throw new Exception($"Migration name should match the \"{migrationNamePattern}\".");
            }

            var shortMigrationName = match.Groups["MigrationName"].Value;
            shortMigrationNames.Add(shortMigrationName, fullMigrationName);
        }

        return shortMigrationNames;
    }
}
