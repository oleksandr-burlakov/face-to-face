using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

public class MeetingMessageConfiguration : IEntityTypeConfiguration<MeetingMessage>
{
    public void Configure(EntityTypeBuilder<MeetingMessage> builder)
    {
        builder
            .HasOne(x => x.Meeting)
            .WithMany(x => x.MeetingMessages)
            .HasForeignKey(x => x.MeetingId);
        builder.Property(x => x.Content).HasColumnType("text");
    }
}
