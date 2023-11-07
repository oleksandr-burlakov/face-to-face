using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

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
