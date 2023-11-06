using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

public class OneWayQuestionConfiguration : IEntityTypeConfiguration<OneWayQuestion>
{
    public string Content { get; set; }

    public void Configure(EntityTypeBuilder<OneWayQuestion> builder)
    {
        builder
            .HasOne(x => x.OneWay)
            .WithMany(x => x.OneWayQuestions)
            .HasForeignKey(x => x.OneWayId);
        builder.Property(x => x.Content).HasColumnType("text");
    }
}
