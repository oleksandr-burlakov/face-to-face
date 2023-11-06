using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class TestAttempt : BaseEntity
{
    public Guid TestId { get; set; }
    public Test Test { get; set; }
    public Guid AssessmentApplyId { get; set; }
    public AssessmentApply AssessmentApply { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TestStatus { get; set; }
    public ICollection<TestUserAnswer> TestUserAnswers { get; set; }
}
