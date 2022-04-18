using Microsoft.EntityFrameworkCore;

using MeetupPlatform.Api.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration
{
    public class ContactsEntityConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> contactEntity)
        {
            contactEntity.ToTable("contacts");

            contactEntity
                .HasKey(contact => contact.Id)
                .HasName("pk_contacts");

            contactEntity
                .Property(contact => contact.Id)
                .IsRequired()
                .HasColumnName("id");

            contactEntity
                .Property(contact => contact.Title)
                .IsRequired()
                .HasColumnName("title");

            contactEntity
                .Property(contact => contact.Value)
                .IsRequired()
                .HasColumnName("value");

            contactEntity
                .HasOne<Meetup>()
                .WithMany(meetup => meetup.Contacts)
                .HasForeignKey(contact => contact.MeetupId)
                .HasConstraintName("fk_contacts_meetups_meetup_id");

            contactEntity
                .Property(contact => contact.MeetupId)
                .IsRequired()
                .HasColumnName("meetup_id");

            contactEntity
                .HasIndex(contact => contact.MeetupId)
                .HasDatabaseName("ix_contacts_meetup_id");
        }
    }
}
