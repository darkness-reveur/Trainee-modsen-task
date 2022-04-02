namespace MeetupPlatformApi.Context.EntitiesConfiguration;

using MeetupPlatformApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MeetupEntityConfiguration : IEntityTypeConfiguration<MeetupEntity>
{
    public void Configure(EntityTypeBuilder<MeetupEntity> meetupEntity)
    {
        meetupEntity.ToTable("meetups");

        meetupEntity
            .HasKey(meetup => meetup.Id)
            .HasName("pk_meetups");

        meetupEntity
            .Property(meetup => meetup.Id)
            .IsRequired()
            .HasColumnName("id");

        meetupEntity
            .Property(meetup => meetup.Name)
            .IsRequired()
            .HasColumnName("name");

        meetupEntity
            .Property(meetup => meetup.Description)
            .IsRequired()
            .HasColumnName("description");

        meetupEntity
            .Property(meetup => meetup.StartTime)
            .IsRequired()
            .HasColumnName("start_time");

        meetupEntity
            .Property(meetup => meetup.EndTime)
            .IsRequired()
            .HasColumnName("end_time");

        meetupEntity
            .Property(meetup => meetup.Location)
            .IsRequired()
            .HasColumnName("location");
            
    }
}

