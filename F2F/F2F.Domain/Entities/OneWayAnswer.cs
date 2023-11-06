using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class OneWayAnswer : BaseEntity
{
    public Guid OneWayQuestionId { get; set; }
    public OneWayQuestion OneWayQuestion { get; set; }
    public Guid OneWayAttemptId { get; set; }
    public OneWayAttempt OneWayAttempt { get; set; }
    public DateTime AttemptTime { get; set; }
    public string? VideoLink { get; set; }
}
