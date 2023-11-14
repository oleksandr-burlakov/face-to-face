namespace F2F.BLL.Models.Question;

public class UpdateQuestionModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid QuestionnaireId { get; set; }
}
