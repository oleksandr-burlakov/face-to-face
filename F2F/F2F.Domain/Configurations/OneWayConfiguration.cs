using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Entities;

public class OneWayConfiguration : IEntityTypeConfiguration<OneWay>
{
    public void Configure(EntityTypeBuilder<OneWay> builder)
    {
        builder.HasOne(x => x.Author).WithMany(x => x.OneWays).HasForeignKey(x => x.AuthorId);
        builder.Property(x => x.Title).HasColumnType("text");
    }
}
