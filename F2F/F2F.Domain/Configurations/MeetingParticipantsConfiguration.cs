using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

public class MeetingParticipantConfiguration : IEntityTypeConfiguration<MeetingParticipant>
{
    void IEntityTypeConfiguration<MeetingParticipant>.Configure(
        EntityTypeBuilder<MeetingParticipant> builder
    )
    {
        builder
            .HasOne(x => x.Meeting)
            .WithMany(x => x.MeetingParticipants)
            .HasForeignKey(x => x.MeetingId);
    }
}
