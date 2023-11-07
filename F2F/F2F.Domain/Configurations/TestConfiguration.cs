using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.Property(x => x.Title).HasColumnType("text");
        builder.HasOne(x => x.Author).WithMany(x => x.Tests).HasForeignKey(x => x.AuthorId);
    }
}
