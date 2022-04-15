namespace MeetupPlatformApi.Persistence.EntitiesConfiguration;

using MeetupPlatformApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class KeywordEntityConfiguration : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> keywordEntity)
    {
        keywordEntity.ToTable("keywords");

        keywordEntity
            .HasKey(keyword => keyword.Id)
            .HasName("pk_keywords");

        keywordEntity
            .Property(keyword => keyword.KeywordName)
            .IsRequired()
            .HasColumnName("keyword_name");

        keywordEntity
            .Property(keyword => keyword.Id)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
    }
}
