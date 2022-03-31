namespace MeetupPlatformApi.Context;

using MeetupPlatformApi.Context.EntitiesConfiguration;
using MeetupPlatformApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class ApplicationContext : DbContext
{
    public DbSet<MeetupEntity> Meetups { get; set; }

    public DbSet<UserEntity> Users { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
