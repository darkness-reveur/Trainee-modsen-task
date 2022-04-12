namespace MeetupPlatformApi.Persistence.EntitiesConfiguration;

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
            .HasMany(p => p.Contacts)
            .WithMany(p => p.Meetups)
            .UsingEntity<Dictionary<string, object>>(
                "meetups_contacts",
                j => j
                    .HasOne<Contact>()
                    .WithMany()
                    .HasForeignKey("contact_id")
                    .HasConstraintName("fk_meetups_contacts_contacts_contact_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Meetup>()
                    .WithMany()
                    .HasForeignKey("meetup_id")
                    .HasConstraintName("fk_meetups_contacts_meetups_meetup_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("contact_id", "meetup_id")
                     .HasName("pk_meetups_contacts");
                    j.HasIndex("meetup_id")
                     .HasName("ix_meetups_contacts_meetup_id");
                });
    }
}
