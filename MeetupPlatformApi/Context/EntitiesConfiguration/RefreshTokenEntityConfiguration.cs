namespace MeetupPlatformApi.Context.EntitiesConfiguration;

using MeetupPlatformApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> refreshTokenEntity)
    {
        refreshTokenEntity.ToTable("refreshTokens");

        refreshTokenEntity
            .HasKey(refreshToken => refreshToken.Id)
            .HasName("pk_refreshTokens");

        refreshTokenEntity
            .Property(refreshToken => refreshToken.Id)
            .IsRequired()
            .HasColumnName("id");

        refreshTokenEntity
            .Property(refreshToken => refreshToken.Expires)
            .IsRequired()
            .HasColumnName("expires");

        refreshTokenEntity
            .HasOne(refreshToken => refreshToken.User)
            .WithMany(user => user.RefreshTokens)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .HasConstraintName("fk_users_refreshTokens");

        refreshTokenEntity
            .Property(refreshToken => refreshToken.UserId)
            .IsRequired()
            .HasColumnName("user_id");
    }
}
