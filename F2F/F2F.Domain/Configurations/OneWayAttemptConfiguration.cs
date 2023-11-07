using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class OneWayAttemptConfiguration : IEntityTypeConfiguration<OneWayAttempt>
{
    public void Configure(EntityTypeBuilder<OneWayAttempt> builder)
    {
        builder
            .HasOne(x => x.OneWay)
            .WithMany(x => x.OneWayAttempts)
            .HasForeignKey(x => x.OneWayId);
        builder
            .HasOne(x => x.AssessmentApply)
            .WithMany(x => x.OneWayAttempts)
            .HasForeignKey(x => x.AssessmentApplyId);
    }
}
