namespace F2F.BLL.Models.Question;

public class QuestionModel
{
    public Guid Id { get; set; }
    public Guid QuestionnaireId { get; set; }
    public string Content { get; set; }
}
