using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class OneWayAnswerConfiguration : IEntityTypeConfiguration<OneWayAnswer>
{
    public string? VideoLink { get; set; }

    public void Configure(EntityTypeBuilder<OneWayAnswer> builder)
    {
        builder
            .HasOne(x => x.OneWayAttempt)
            .WithMany(x => x.OneWayAnswers)
            .HasForeignKey(x => x.OneWayAttemptId);
        builder.Property(x => x.VideoLink).HasMaxLength(256);
    }
}
