using F2F.DLL.Entities;

namespace F2F.BLL.Models.Meetings;

public class MeetingModel
{
    public Guid Id { get; set; }
    public string? RecordLink { get; set; }
    public string ParticipantsEmail { get; set; }
    public DateTime? AssignedTime { get; set; }
    public Guid OwnerId { get; set; }
    public bool AllowedConnectWithoutHost { get; set; }
    public int? MaxAllowedParticipantsNumber { get; set; }
    public bool SaveChat { get; set; }
    public bool IsFinished { get; set; }
}
