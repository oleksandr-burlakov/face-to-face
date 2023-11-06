using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class Question : BaseEntity
{
    public int Order { get; set; }
    public string Content { get; set; }
    public Guid QuestionnaireId { get; set; }
    public Questionnaire Questionnaire { get; set; }
}
