using F2F.DLL.Entities.Base;

namespace F2F.DLL.Entities;

public class MeetingParticipant : BaseEntity
{
    public Guid MeetingId { get; set; }
    public Meeting Meeting { get; set; }
    public string ParticipantId { get; set; }
    public string UserName { get; set; }
}
