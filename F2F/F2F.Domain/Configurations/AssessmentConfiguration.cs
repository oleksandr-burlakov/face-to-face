using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder.HasOne(x => x.Author).WithMany(x => x.Assessments).HasForeignKey(x => x.AuthorId);
        builder.Property(x => x.Content).HasColumnType("text");
        builder.Property(x => x.Title).HasMaxLength(255);
    }
}
