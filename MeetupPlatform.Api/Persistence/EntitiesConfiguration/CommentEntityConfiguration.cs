namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration;

using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain.Comments;
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
            .HasDiscriminator<string>("comment_type");

        commentEntity
            .Property("comment_type")
            .HasDefaultValue(Comments.Root)
            .IsRequired();
    }
}
