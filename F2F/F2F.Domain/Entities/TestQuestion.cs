using F2F.DLL.Entities.Base;

namespace F2F.DLL.Entities;

public class TestQuestion : BaseEntity
{
    public Guid TestSectionId { get; set; }
    public TestSection TestSection { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public int QuestionType { get; set; }
    public decimal MaxPoints { get; set; }
    public ICollection<TestAnswer> TestAnswers { get; set; }
}
