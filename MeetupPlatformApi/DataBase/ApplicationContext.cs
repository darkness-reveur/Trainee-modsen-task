using MeetupPlatformApi.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatformApi.DataBase;

public class ApplicationContext : DbContext
{
    public DbSet<MeetupEntity> Meetups { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    { }
}
