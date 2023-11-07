using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F2F.DLL.Configuration;

public class MeetingQuestionPointConfiguration : IEntityTypeConfiguration<MeetingQuestionPoint>
{
    void IEntityTypeConfiguration<MeetingQuestionPoint>.Configure(
        EntityTypeBuilder<MeetingQuestionPoint> builder
    )
    {
        builder
            .HasOne(x => x.Meeting)
            .WithMany(m => m.QuestionPoints)
            .HasForeignKey(x => x.MeetingId);
    }
}
