namespace F2F.BLL.Models.MeetingParticipant;

public class DeleteParticipantResult
{
    public Guid? MeetingId { get; set; }
    public bool IsAnyoneLeft { get; set; }
}
