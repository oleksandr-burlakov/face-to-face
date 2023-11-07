using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class TestSectionConfiguration : IEntityTypeConfiguration<TestSection>
{
    public void Configure(EntityTypeBuilder<TestSection> builder)
    {
        builder.Property(x => x.Title).HasColumnType("text");
        builder.HasOne(x => x.Test).WithMany(x => x.TestSections).HasForeignKey(x => x.TestId);
    }
}
