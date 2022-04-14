namespace MeetupPlatform.Api.Persistence.Context;

using System.Reflection;
using MeetupPlatform.Api.Domain;
using MeetupPlatform.Api.Domain.Comments;
using MeetupPlatform.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Meetup> Meetups { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<PlainUser> PlainUsers { get; set; }

    public DbSet<Organizer> Organizers { get; set; }

    public DbSet<ReplyComment> ReplyComments { get; set; }

    public DbSet<RootComment> RootComments { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
