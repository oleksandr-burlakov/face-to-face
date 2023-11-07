using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

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
