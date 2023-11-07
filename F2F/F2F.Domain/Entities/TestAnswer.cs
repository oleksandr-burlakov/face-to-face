using F2F.DLL.Entities.Base;

namespace F2F.DLL.Entities;

public class TestAnswer : BaseEntity
{
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
    public int Order { get; set; }
    public bool IsCorrect { get; set; }
    public string Content { get; set; }
}
