using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class TestUserAnswerConfiguration : IEntityTypeConfiguration<TestUserAnswer>
{
    public void Configure(EntityTypeBuilder<TestUserAnswer> builder)
    {
        builder
            .HasOne(x => x.TestAttempt)
            .WithMany(x => x.TestUserAnswers)
            .HasForeignKey(x => x.TestAttemptId);
        builder.Property(x => x.ManualAnswer).HasColumnType("text");
    }
}
