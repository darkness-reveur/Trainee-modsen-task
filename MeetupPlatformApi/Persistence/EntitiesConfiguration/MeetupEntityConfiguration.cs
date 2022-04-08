namespace MeetupPlatformApi.Persistence.EntitiesConfiguration;

using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Domain.Users;
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
            .WithMany(organizer => organizer.Meetups)
            .OnDelete(DeleteBehavior.SetNull)
            .HasForeignKey(meetup => meetup.OrganizerId)
            .HasConstraintName("fk_users_meetups_organizer_id");

        meetupEntity
            .Property(meetup => meetup.OrganizerId)
            .IsRequired()
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("organizer_id");

        meetupEntity
            .HasIndex(meetup => meetup.OrganizerId)
            .HasName("ix_meetups_organizer_id");

        meetupEntity
            .HasMany(meetup => meetup.Users)
            .WithMany(plainUser => plainUser.Meetups)
            .UsingEntity<Dictionary<string, object>>(
            "meetups_plain_users",
            join => join
                    .HasOne<PlainUser>()
                    .WithMany()
                    .HasForeignKey("user_id")
                    .HasConstraintName("fk_meetups_plain_users_plain_users_user_id")
                    .OnDelete(DeleteBehavior.Cascade),
            join => join
                    .HasOne<Meetup>()
                    .WithMany()
                    .HasForeignKey("meetup_id")
                    .HasConstraintName("fk_meetups_plain_users_meetups_meetup_id")
                    .OnDelete(DeleteBehavior.Cascade));

        meetupEntity
            .HasMany(meetup => meetup.Users)
            .WithMany(plainUser => plainUser.Meetups)
            .UsingEntity<Dictionary<string, object>>(
            "meetups_plain_users",
            join => join
                    .HasIndex("user_id")
                    .HasName("ix_meetups_plain_users_user_id"));

        meetupEntity
            .HasMany(meetup => meetup.Users)
            .WithMany(plainUser => plainUser.Meetups)
            .UsingEntity<Dictionary<string, object>>(
            "meetups_plain_users",
            join => join
                    .HasIndex("meetup_id")
                    .HasName("ix_meetups_plain_users_meetup_id"));

        meetupEntity
            .HasMany(meetup => meetup.Users)
            .WithMany(plainUser => plainUser.Meetups)
            .UsingEntity<Dictionary<string, object>>(
            "meetups_plain_users",
            join => join
                    .HasKey("user_id", "meetup_id")
                    .HasName("pk_meetups_plain_users"));
    }
}
