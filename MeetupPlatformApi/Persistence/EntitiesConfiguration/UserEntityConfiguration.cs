namespace MeetupPlatformApi.Persistence.EntitiesConfiguration;

using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> userEntity)
    {
        userEntity.ToTable("users");

        userEntity
            .HasKey(user => user.Id)
            .HasName("pk_users");

        userEntity
            .Property(user => user.Id)
            .IsRequired()
            .HasColumnName("id");

        userEntity
            .HasIndex(user => user.Username)
            .IsUnique()
            .HasDatabaseName("ux_users_username");

        userEntity
            .Property(user => user.Username)
            .IsRequired()
            .HasColumnName("username");

        userEntity
            .Property(user => user.Password)
            .IsRequired()
            .HasColumnName("password");

        userEntity
            .Ignore(user => user.Role);

        userEntity
            .HasDiscriminator<string>("role");

        userEntity
            .Property("role")
            .HasDefaultValue(Roles.PlainUser)
            .IsRequired();
    }
}
