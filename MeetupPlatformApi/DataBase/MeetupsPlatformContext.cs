using MeetupPlatformApi.DTO;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatformApi.DataBase
{
    public class MeetupsPlatformContext : DbContext
    {
        public MeetupsPlatformContext(DbContextOptions<MeetupsPlatformContext> options)
            : base(options)
        {}
        
        public DbSet<MeetupDTO> Meetups { get; set; }
    }
}
