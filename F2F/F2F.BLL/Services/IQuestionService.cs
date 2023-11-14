using F2F.BLL.Models.Question;

namespace F2F.BLL.Services;

public interface IQuestionService
{
    public Task<Guid> InsertAsync(AddQuestionModel model);
    public Task UpdateAsync(UpdateQuestionModel model);
    public Task<IEnumerable<QuestionModel>> GetByQuestionnaireIdAsync(Guid questionnaireId);
    public Task DeleteAsync(Guid id);
}
