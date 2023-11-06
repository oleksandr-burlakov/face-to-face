using F2F.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.Domain.Configuration;

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
