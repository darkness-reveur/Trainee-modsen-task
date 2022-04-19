namespace MeetupPlatform.Api.Persistence.EntitiesConfiguration;

using MeetupPlatform.Api.Domain;
using MeetupPlatform.Api.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RootCommentEntityConfiguration : IEntityTypeConfiguration<RootComment>
{
    public void Configure(EntityTypeBuilder<RootComment> rootCommentEntity)
    {
        rootCommentEntity
            .HasOne<Meetup>()
            .WithMany(meetup => meetup.Comments)
            .HasForeignKey(rootComment => rootComment.MeetupId)
            .HasConstraintName("fk_root_comments_meetups_meetup_id");

        rootCommentEntity
            .Property(rootComment => rootComment.MeetupId)
            .IsRequired(false)
            .HasColumnName("meetup_id");

        rootCommentEntity
            .HasIndex(rootComment => rootComment.MeetupId)
            .HasDatabaseName("ix_root_comments_meetup_id");
    }
}
