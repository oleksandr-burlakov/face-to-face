using F2F.DLL.Entities.Base;

namespace F2F.DLL.Entities;

public class TestUserAnswer : BaseEntity
{
    public Guid TestAttemptId { get; set; }
    public TestAttempt TestAttempt { get; set; }
    public Guid? TestAnswerId { get; set; }
    public TestAnswer? TestAnswer { get; set; }
    public string ManualAnswer { get; set; }
}
