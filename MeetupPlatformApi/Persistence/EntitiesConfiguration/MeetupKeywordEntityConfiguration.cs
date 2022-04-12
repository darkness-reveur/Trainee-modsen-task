namespace MeetupPlatformApi.Persistence.EntitiesConfiguration;

using MeetupPlatformApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MeetupKeywordEntityConfiguration : IEntityTypeConfiguration<MeetupKeyword>
{
    public void Configure(EntityTypeBuilder<MeetupKeyword> meetupKeywordEntity)
    {
        meetupKeywordEntity.ToTable("meetups_keywords");

        meetupKeywordEntity
            .HasKey(meetupKeyword => meetupKeyword.Id)
            .HasName("pk_meetups_keywords");

        meetupKeywordEntity
            .Property(meetupKeyword => meetupKeyword.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        meetupKeywordEntity
            .Property(meetupKeyword => meetupKeyword.KeywordId)
            .IsRequired()
            .HasColumnName("keyword_id");

        meetupKeywordEntity
            .HasIndex(meetupKeyword => meetupKeyword.KeywordId)
            .HasName("ix_meetups_keywords_keyword_id");

        meetupKeywordEntity
            .Property(meetupKeyword => meetupKeyword.MeetupId)
            .IsRequired()
            .HasColumnName("meetup_id");

        meetupKeywordEntity
            .HasIndex(meetupKeyword => meetupKeyword.MeetupId)
            .HasName("ix_meetups_keywords_meetup_id");

        meetupKeywordEntity
            .HasOne(meetupKeyword => meetupKeyword.Meetup)
            .WithMany()
            .HasForeignKey(meetupKeyword => meetupKeyword.MeetupId)
            .HasConstraintName("fk_meetups_meetups_keywords_meetup_id");

        meetupKeywordEntity
            .HasOne(meetupKeyword => meetupKeyword.Keyword)
            .WithMany()
            .HasForeignKey(meetupKeyword => meetupKeyword.KeywordId)
            .HasConstraintName("fk_keywords_meetups_keywords_keyword_id");
    }
}
