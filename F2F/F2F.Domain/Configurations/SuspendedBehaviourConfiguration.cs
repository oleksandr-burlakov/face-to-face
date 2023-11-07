using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class SuspendedBehaviourConfiguration : IEntityTypeConfiguration<SuspendedBehaviour>
{
    public void Configure(EntityTypeBuilder<SuspendedBehaviour> builder)
    {
        builder
            .HasOne(x => x.Meeting)
            .WithMany(x => x.SuspendedBehaviours)
            .HasForeignKey(x => x.MeetingId);
    }
}
