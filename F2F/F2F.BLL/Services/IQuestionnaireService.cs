using F2F.BLL.Models.Questionnaire;

namespace F2F.BLL.Services;

public interface IQuestionnaireService
{
    public Task<Guid> AddAsync(AddQuestionnaireModel model);
    public Task<IEnumerable<GetMyQuestionnairesResponseModel>> GetMyAsync(
        GetMyQuestionnairesModel model
    );
    public Task<GetMyQuestionnairesResponseModel> GetAsync(Guid id);

    public Task<Guid> UpdateAsync(UpdateQuestionnaireModel model);
    public Task DeleteAsync(Guid id);
}
