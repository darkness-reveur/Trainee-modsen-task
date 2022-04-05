namespace MeetupPlatformApi.Context.EntitiesConfiguration;

using MeetupPlatformApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> refreshTokenEntity)
    {
        refreshTokenEntity.ToTable("refresh_tokens");

        refreshTokenEntity
            .HasKey(refreshToken => refreshToken.Id)
            .HasName("pk_refresh_tokens");

        refreshTokenEntity
            .Property(refreshToken => refreshToken.Id)
            .IsRequired()
            .HasColumnName("id");

        refreshTokenEntity
            .HasOne(refreshToken => refreshToken.User)
            .WithMany(user => user.RefreshTokens)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .HasConstraintName("fk_users_refresh_tokens_user_id");

        refreshTokenEntity
            .Property(refreshToken => refreshToken.UserId)
            .IsRequired()
            .HasColumnName("user_id");
    }
}
