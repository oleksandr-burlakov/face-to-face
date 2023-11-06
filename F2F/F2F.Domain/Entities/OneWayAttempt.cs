using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class OneWayAttempt : BaseEntity
{
    public Guid OneWayId { get; set; }
    public OneWay OneWay { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public ICollection<OneWayAnswer> OneWayAnswers { get; set; }
    public Guid AssessmentApplyId { get; set; }
    public AssessmentApply AssessmentApply { get; set; }
}
