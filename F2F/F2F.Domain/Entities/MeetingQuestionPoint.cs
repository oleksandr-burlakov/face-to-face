using F2F.DLL.Entities.Base;

namespace F2F.DLL.Entities;

public class MeetingQuestionPoint : BaseEntity
{
    public Guid MeetingId { get; set; }
    public Meeting Meeting { get; set; }
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
    public int? Score { get; set; }
    public DateTime TimeOfAnswer { get; set; }
}
