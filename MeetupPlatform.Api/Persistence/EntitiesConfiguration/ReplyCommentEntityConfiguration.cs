namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration;

using MeetupPlatform.Api.Domain.Comments;
using MeetupPlatform.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ReplyCommentEntityConfiguration : IEntityTypeConfiguration<ReplyComment>
{
    public void Configure(EntityTypeBuilder<ReplyComment> replyCommentEntity)
    {
        replyCommentEntity.ToTable("reply_comments");

        replyCommentEntity
            .HasKey(replyComment => replyComment.Id)
            .HasName("pk_reply_comments");

        replyCommentEntity
            .Property(replyComment => replyComment.Id)
            .IsRequired()
            .HasColumnName("id");

        replyCommentEntity
            .Property(replyComment => replyComment.Text)
            .IsRequired()
            .HasColumnName("text");

        replyCommentEntity
            .HasOne<RootComment>()
            .WithMany(rootComment => rootComment.ReplyComments)
            .HasForeignKey(replyComment => replyComment.RootCommentId)
            .HasConstraintName("fk_reply_comments_root_comments_root_comment_id");

        replyCommentEntity
            .Property(replyComment => replyComment.RootCommentId)
            .IsRequired()
            .HasColumnName("root_comment_id");

        replyCommentEntity
            .HasIndex(replyComment => replyComment.RootCommentId)
            .HasName("ix_reply_comments_root_comment_id");

        replyCommentEntity
            .HasOne<PlainUser>()
            .WithMany(plainUser => plainUser.ReplyComments)
            .HasForeignKey(replyComment => replyComment.PlainUserId)
            .HasConstraintName("fk_reply_comments_users_plain_user_id");

        replyCommentEntity
            .Property(replyComment => replyComment.PlainUserId)
            .IsRequired()
            .HasColumnName("plain_user_id");

        replyCommentEntity
            .HasIndex(replyComment => replyComment.PlainUserId)
            .HasName("ix_reply_comments_plain_user_id");
    }
}
