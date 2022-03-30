using MeetupPlatformApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupPlatformApi.Context.EntitiesConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id)
                .HasName("pk_users");

            builder.Property(u => u.Id)
                .IsRequired()
                .HasColumnName("pk_users");

            builder.HasIndex(u => u.Username)
                .HasName("ux_users_username")
                .IsUnique();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasColumnName("ux_users_username");

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();
        }
    }
}
