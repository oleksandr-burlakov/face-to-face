namespace F2F.BLL.Models.Questionnaire;

public class GetMyQuestionnairesModel
{
    public Guid AuthorId { get; set; }
}

public class GetMyQuestionnairesResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}
