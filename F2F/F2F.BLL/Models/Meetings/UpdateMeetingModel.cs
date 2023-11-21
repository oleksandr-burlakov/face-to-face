namespace F2F.BLL.Models.Meetings;

public class UpdateMeetingModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? ParticipantsEmail { get; set; }
    public DateTime? AssignedTime { get; set; }
    public bool AllowedConnectWithoutHost { get; set; }
    public int? MaxAllowedParticipantsNumber { get; set; }
    public bool SaveChat { get; set; }
    public Guid? PreferableQuestionnaireId { get; set; }
}
