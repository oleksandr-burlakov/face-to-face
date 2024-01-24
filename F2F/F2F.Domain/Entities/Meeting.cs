using F2F.DLL.Entities.Base;

namespace F2F.DLL.Entities;

public class Meeting : BaseEntity
{
    public string Title { get; set; }
    public string? RecordLink { get; set; }
    public string? ParticipantsEmail { get; set; }
    public ICollection<MeetingParticipant> MeetingParticipants { get; set; }
    public DateTime? AssignedTime { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public bool AllowedConnectWithoutHost { get; set; }
    public int? MaxAllowedParticipantsNumber { get; set; }
    public bool SaveChat { get; set; }
    public bool IsFinished { get; set; }
    public Guid? PreferableQuestionnaireId { get; set; }
    public Questionnaire? PreferableQuestionnaire { get; set; }
}