using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class MeetingParticipantConfiguration : IEntityTypeConfiguration<MeetingParticipant>
{
    void IEntityTypeConfiguration<MeetingParticipant>.Configure(
        EntityTypeBuilder<MeetingParticipant> builder
    ) { }
}
