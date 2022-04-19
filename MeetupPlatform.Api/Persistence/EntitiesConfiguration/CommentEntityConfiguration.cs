namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration;

using MeetupPlatform.Api.Domain.Comments;
using MeetupPlatform.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> commentEntity)
    {
        commentEntity.ToTable("comments");

        commentEntity
            .HasKey(comment => comment.Id)
            .HasName("pk_comments");

        commentEntity
            .Property(comment => comment.Id)
            .IsRequired()
            .HasColumnName("id");

        commentEntity
            .Property(comment => comment.Text)
            .IsRequired()
            .HasColumnName("text");

        commentEntity
            .HasOne<PlainUser>()
            .WithMany(plainUser => plainUser.Comments)
            .HasForeignKey(comment => comment.AuthorId)
            .HasConstraintName("fk_comments_users_author_id");

        commentEntity
            .Property(comment => comment.AuthorId)
            .IsRequired()
            .HasColumnName("author_id");

        commentEntity
            .HasIndex(comment => comment.AuthorId)
            .HasDatabaseName("ix_comments_author_id");

        commentEntity
            .Property(comment => comment.Posted)
            .IsRequired()
            .HasColumnName("posted");

        commentEntity
            .HasDiscriminator<string>("comment_type");
    }
}
