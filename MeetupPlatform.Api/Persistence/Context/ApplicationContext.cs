namespace MeetupPlatform.Api.Persistence.Context;

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MeetupPlatform.Api.Domain;

public class ApplicationContext : DbContext
{
    public DbSet<Meetup> Meetups { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
