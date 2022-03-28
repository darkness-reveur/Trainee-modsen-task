using MeetupPlatformApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatformApi.Context;

public class ApplicationContext : DbContext
{
    public DbSet<MeetupEntity> Meetups { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    { }
}
