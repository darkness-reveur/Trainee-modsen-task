using MeetupPlatformApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupPlatformApi.Context.EntitiesConfiguration
{
    public class MeetupEntityConfiguration : IEntityTypeConfiguration<MeetupEntity>
    {
        public void Configure(EntityTypeBuilder<MeetupEntity> builder)
        {
            builder.ToTable("meetups");

            builder.HasKey(m => m.Id)
                .HasName("pk_meetups");

            builder.Property(m => m.Id)
                .IsRequired()
                .HasColumnName("pk_meetups");

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(4000)
                .HasColumnName("description");

            builder.Property(m => m.StartTime)
                .IsRequired()
                .HasColumnName("start_time");

            builder.Property(m => m.EndTime)
                .IsRequired()
                .HasColumnName("end_time");
        }
    }
}
