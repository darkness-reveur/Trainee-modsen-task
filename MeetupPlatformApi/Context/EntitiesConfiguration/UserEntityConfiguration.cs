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
            .HasName("ux_users_username")
            .IsUnique();

        userEntity
            .Property(user => user.Username)
            .IsRequired()
            .HasColumnName("username");

        userEntity
            .Property(user => user.Password)
            .HasColumnName("password")
            .IsRequired();
    }
}

