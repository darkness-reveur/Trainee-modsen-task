namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration;

using MeetupPlatform.Api.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RaplyCommentEntityConfiguration : IEntityTypeConfiguration<ReplyComment>
{
    public void Configure(EntityTypeBuilder<ReplyComment> replyCommentEntity)
    {
        replyCommentEntity.ToTable("reply_comments");

        replyCommentEntity
            .HasOne<RootComment>()
            .WithMany(rootComment => rootComment.ReplyComments)
            .HasForeignKey(replyComment => replyComment.RootCommentId)
            .HasConstraintName("fk_reply_comments_root_comments_root_comment_id");

        replyCommentEntity
            .Property(replyComment => replyComment.RootCommentId)
            .IsRequired()
            .HasColumnName("root_comment_id");
    }
}
