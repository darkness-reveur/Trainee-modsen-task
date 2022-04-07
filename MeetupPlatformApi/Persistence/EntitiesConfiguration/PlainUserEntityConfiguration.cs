namespace MeetupPlatformApi.Persistence.EntitiesConfiguration;

using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PlainUserEntityConfiguration : IEntityTypeConfiguration<PlainUser>
{
    public void Configure(EntityTypeBuilder<PlainUser> plainUserEntity)
    {
        plainUserEntity
            .HasOne<Meetup>()
            .WithMany(meetup => meetup.Users)
            .OnDelete(DeleteBehavior.SetNull)
            .HasForeignKey(plainUser => plainUser.MeetupId)
            .HasConstraintName("fk_meetups_users_meetup_id");

        plainUserEntity
            .Property(plainUser => plainUser.MeetupId)
            .HasColumnName("meetup_id");
    }
}
