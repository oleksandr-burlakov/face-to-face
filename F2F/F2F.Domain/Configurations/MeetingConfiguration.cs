using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.Property(x => x.RecordLink).HasMaxLength(256);
        builder.Property(x => x.MeetingLink).HasMaxLength(256);
        builder.Property(x => x.ParticipantsEmail).HasColumnType("text");
        builder.HasOne(x => x.Owner).WithMany(x => x.MyMeetings).HasForeignKey(x => x.OwnerId);
    }
}
