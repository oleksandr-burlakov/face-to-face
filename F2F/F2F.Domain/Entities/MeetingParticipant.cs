using F2F.DLL.Entities.Base;

namespace F2F.DLL.Entities;

public class MeetingParticipant : BaseEntity
{
    public Guid MeetingId { get; set; }
    public Meeting Meeting { get; set; }
    public Guid ParticipantId { get; set; }
    public User Participant { get; set; }
}
