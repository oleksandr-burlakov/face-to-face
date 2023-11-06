using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
{
    public void Configure(EntityTypeBuilder<Questionnaire> builder)
    {
        builder.Property(x => x.Title).HasColumnType("text");
        builder
            .HasOne(x => x.Author)
            .WithMany(x => x.Questionnaires)
            .HasForeignKey(x => x.AuthorId);
    }
}
