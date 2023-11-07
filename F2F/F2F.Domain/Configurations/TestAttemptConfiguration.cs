using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

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
