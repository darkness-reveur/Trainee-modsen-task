using MeetupPlatformApi.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatformApi.DataBase;

public class MeetupsPlatformContext : DbContext
{
    public MeetupsPlatformContext(DbContextOptions<MeetupsPlatformContext> options)
        : base(options)
    { }

    public DbSet<MeetupEntity> Meetups { get; set; }
}
