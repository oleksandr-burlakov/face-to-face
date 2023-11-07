using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class TestAnswerConfiguration : IEntityTypeConfiguration<TestAnswer>
{
    public void Configure(EntityTypeBuilder<TestAnswer> builder)
    {
        builder.Property(x => x.Content).HasColumnType("text");
    }
}
