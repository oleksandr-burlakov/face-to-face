using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class AssessmentApply : BaseEntity
{
    public Guid AssessmentId { get; set; }
    public Assessment Assessment { get; set; }
    public DateTime? AllowedTestEndDate { get; set; }
    public DateTime? AllowedOneWayEndDate { get; set; }
    public Guid UserId { get; set; }
    public User Applicant { get; set; }
    public Guid? MeetingId { get; set; }
    public Meeting? Meeting { get; set; }
    public string Feedback { get; set; }
    public int Stage { get; set; }
    public ICollection<TestAttempt> TestAttempts { get; set; }
    public ICollection<OneWayAttempt> OneWayAttempts { get; set; }
}
