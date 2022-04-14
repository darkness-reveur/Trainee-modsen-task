namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration;

using MeetupPlatform.Api.Domain;
using MeetupPlatform.Api.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RootCommentEntityConfiguration : IEntityTypeConfiguration<RootComment>
{
    public void Configure(EntityTypeBuilder<RootComment> rootCommentEntity)
    {
        rootCommentEntity.ToTable("root_comments");

        rootCommentEntity
            .HasKey(rootComment => rootComment.Id)
            .HasName("pk_root_comments");

        rootCommentEntity
            .Property(rootComment => rootComment.Id)
            .IsRequired()
            .HasColumnName("id");

        rootCommentEntity
            .Property(rootComment => rootComment.Text)
            .IsRequired()
            .HasColumnName("text");

        rootCommentEntity
            .HasOne<Meetup>()
            .WithMany(meetup => meetup.RootComments)
            .HasForeignKey(rootComment => rootComment.MeetupId)
            .HasConstraintName("fk_root_comments_meetups_meetup_id");

        rootCommentEntity
            .Property(rootComment => rootComment.MeetupId)
            .IsRequired()
            .HasColumnName("meetup_id");

        rootCommentEntity
            .HasIndex(rootComment => rootComment.MeetupId)
            .HasName("ix_root_comments_meetup_id");
    }
}
