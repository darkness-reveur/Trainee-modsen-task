namespace MeetupPlatformApi.Context.EntitiesConfiguration;

using MeetupPlatformApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> userEntity)
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
    }
}

