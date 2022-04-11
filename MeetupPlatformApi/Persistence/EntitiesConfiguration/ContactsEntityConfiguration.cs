using Microsoft.EntityFrameworkCore;

using MeetupPlatformApi.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupPlatformApi.Persistence.EntitiesConfiguration
{
    public class ContactsEntityConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> meetupEntity)
        {
            meetupEntity.ToTable("contacts");

            meetupEntity
                .HasKey(contact => contact.Id)
                .HasName("pk_contacts");

            meetupEntity
                .Property(contact => contact.Id)
                .IsRequired()
                .HasColumnName("id");

            meetupEntity
                .Property(contact => contact.Title)
                .IsRequired()
                .HasColumnName("title");

            meetupEntity
                .Property(contact => contact.Value)
                .IsRequired()
                .HasColumnName("value");
        }
    }
}
