using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

public class TestAttemptConfiguration : IEntityTypeConfiguration<TestAttempt>
{
    public void Configure(EntityTypeBuilder<TestAttempt> builder)
    {
        builder.HasOne(x => x.Test).WithMany(x => x.TestAttempts).HasForeignKey(x => x.TestId);
        builder
            .HasOne(x => x.AssessmentApply)
            .WithMany(x => x.TestAttempts)
            .HasForeignKey(x => x.AssessmentApplyId);
    }
}
