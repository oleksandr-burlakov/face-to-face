using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

public class AssessmentApplyConfiguration : IEntityTypeConfiguration<AssessmentApply>
{
    public void Configure(EntityTypeBuilder<AssessmentApply> builder)
    {
        builder
            .HasOne(x => x.Assessment)
            .WithMany(x => x.Applies)
            .HasForeignKey(x => x.AssessmentId);
        builder
            .HasOne(x => x.Applicant)
            .WithMany(x => x.AssessmentApplies)
            .HasForeignKey(x => x.UserId);
        builder.Property(x => x.Feedback).HasColumnType("text");
    }
}
