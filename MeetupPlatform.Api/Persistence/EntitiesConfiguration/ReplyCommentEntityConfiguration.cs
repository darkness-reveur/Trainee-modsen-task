namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration;

using MeetupPlatform.Api.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ReplyCommentEntityConfiguration : IEntityTypeConfiguration<ReplyComment>
{
    public void Configure(EntityTypeBuilder<ReplyComment> replyCommentEntity)
    {
        replyCommentEntity
            .HasOne<RootComment>()
            .WithMany(rootComment => rootComment.Replies)
            .HasForeignKey(replyComment => replyComment.RootCommentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_reply_comments_root_comments_root_comment_id");

        replyCommentEntity
            .Property(replyComment => replyComment.RootCommentId)
            .IsRequired(false)
            .HasColumnName("root_comment_id");

        replyCommentEntity
            .HasIndex(replyComment => replyComment.RootCommentId)
            .HasDatabaseName("ix_reply_comments_root_comment_id");
    }
}
