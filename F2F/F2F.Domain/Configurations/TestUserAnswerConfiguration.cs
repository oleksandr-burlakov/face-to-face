using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

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
