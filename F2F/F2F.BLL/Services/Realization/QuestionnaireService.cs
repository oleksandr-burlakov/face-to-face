using AutoMapper;
using F2F.BLL.Models.Questionnaire;
using F2F.DLL;
using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace F2F.BLL.Services.Realization;

internal class QuestionnaireService : IQuestionnaireService
{
    private readonly F2FContext _context;
    private readonly IMapper _mapper;

    public QuestionnaireService(F2FContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(AddQuestionnaireModel model)
    {
        var questionnaire = _mapper.Map<Questionnaire>(model);
        await _context.AddAsync(questionnaire);
        await _context.SaveChangesAsync();
        return questionnaire.Id;
    }

    public async Task<Guid> UpdateAsync(UpdateQuestionnaireModel model)
    {
        var existedQuestionnaire = await _context.Questionnaires.FirstOrDefaultAsync(
            x => x.Id == model.Id
        );
        existedQuestionnaire.Title = model.Title;
        _context.Entry(existedQuestionnaire).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return model.Id;
    }

    public async Task<IEnumerable<GetMyQuestionnairesResponseModel>> GetMyAsync(
        GetMyQuestionnairesModel model
    )
    {
        var questionnaires = await _context.Questionnaires
            .Where(q => q.AuthorId == model.AuthorId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<GetMyQuestionnairesResponseModel>>(questionnaires);
    }

    public async Task DeleteAsync(Guid id)
    {
        var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(x => x.Id == id);
        _context.Questionnaires.Remove(questionnaire);
        await _context.SaveChangesAsync();
    }

    public async Task<GetMyQuestionnairesResponseModel> GetAsync(Guid id)
    {
        var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(q => q.Id == id);
        return _mapper.Map<GetMyQuestionnairesResponseModel>(questionnaire);
    }
}
