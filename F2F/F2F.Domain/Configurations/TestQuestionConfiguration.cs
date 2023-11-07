using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
{
    public void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        builder
            .HasOne(x => x.TestSection)
            .WithMany(x => x.TestQuestions)
            .HasForeignKey(x => x.TestSectionId);
        builder.Property(x => x.Content).HasColumnType("text");
    }
}
