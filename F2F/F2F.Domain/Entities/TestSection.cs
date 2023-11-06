using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class TestSection : BaseEntity
{
    public string Title { get; set; }
    public Guid TestId { get; set; }
    public Test Test { get; set; }
    public int Order { get; set; }
    public ICollection<TestQuestion> TestQuestions { get; set; }
}
