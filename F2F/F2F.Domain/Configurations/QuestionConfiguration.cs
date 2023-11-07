using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(x => x.Content).HasColumnType("text");
        builder
            .HasOne(x => x.Questionnaire)
            .WithMany(x => x.Questions)
            .HasForeignKey(x => x.QuestionnaireId);
    }
}
