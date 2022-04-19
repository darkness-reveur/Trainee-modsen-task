namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration;

using MeetupPlatform.Api.Domain;
using MeetupPlatform.Api.Domain.Users;
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
            .HasOne<Organizer>()
            .WithMany(organizer => organizer.OrganizedMeetups)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(meetup => meetup.OrganizerId)
            .HasConstraintName("fk_users_meetups_organizer_id");

        meetupEntity
            .Property(meetup => meetup.OrganizerId)
            .IsRequired()
            .HasColumnName("organizer_id");

        meetupEntity
            .HasIndex(meetup => meetup.OrganizerId)
            .HasDatabaseName("ix_meetups_organizer_id");

        meetupEntity
            .HasMany(meetup => meetup.SignedUpUsers)
            .WithMany(plainUser => plainUser.MeetupsSignedUpFor)
            .UsingEntity<Dictionary<string, object>>(
                "meetups_users_signups",
                join => join
                        .HasOne<PlainUser>()
                        .WithMany()
                        .HasForeignKey("user_id")
                        .HasConstraintName("fk_meetups_users_signups_plain_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade),
                join => join
                        .HasOne<Meetup>()
                        .WithMany()
                        .HasForeignKey("meetup_id")
                        .HasConstraintName("fk_meetups_users_signups_meetups_meetup_id")
                        .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.HasKey("user_id", "meetup_id").HasName("pk_meetups_users_signup");
                    join.HasIndex("user_id").HasDatabaseName("ix_meetups_users_signups_user_id");
                    join.HasIndex("meetup_id").HasDatabaseName("ix_meetups_users_signups_meetup_id");
                });

        meetupEntity
            .Property(meetup => meetup.Location)
            .IsRequired()
            .HasColumnName("location");
    }
}
