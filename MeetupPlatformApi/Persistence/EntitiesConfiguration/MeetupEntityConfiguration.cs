﻿namespace MeetupPlatformApi.Persistence.EntitiesConfiguration;

using MeetupPlatformApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MeetupEntityConfiguration : IEntityTypeConfiguration<Meetup>
{
    public void Configure(EntityTypeBuilder<Meetup> meetupEntity)
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
            .Property(meetup => meetup.Title)
            .IsRequired()
            .HasColumnName("title");

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
            .HasOne<User>()
            .WithMany(user => user.Meetups)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(meetup => meetup.UserId)
            .HasConstraintName("fk_users_meetups_user_id");

        meetupEntity
            .Property(meetup => meetup.UserId)
            .IsRequired()
            .HasColumnName("organizer_id");
    }
}
