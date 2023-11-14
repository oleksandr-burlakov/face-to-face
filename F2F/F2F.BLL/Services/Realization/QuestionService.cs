using AutoMapper;
using F2F.BLL.Models.Question;
using F2F.DLL;
using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace F2F.BLL.Services.Realization;

public class QuestionService : IQuestionService
{
    private readonly IMapper _mapper;
    private readonly F2FContext _context;

    public QuestionService(IMapper mapper, F2FContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task DeleteAsync(Guid id)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<QuestionModel>> GetByQuestionnaireIdAsync(Guid questionnaireId)
    {
        var question = await _context.Questions
            .Where(q => q.QuestionnaireId == questionnaireId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<QuestionModel>>(question);
    }

    public async Task<Guid> InsertAsync(AddQuestionModel model)
    {
        var question = _mapper.Map<Question>(model);
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return question.Id;
    }

    public async Task UpdateAsync(UpdateQuestionModel model)
    {
        var question = _mapper.Map<Question>(model);
        var questionToUpdate = _context.Questions.FirstOrDefault(x => x.Id == question.Id);
        if (questionToUpdate != null)
        {
            questionToUpdate.Content = question.Content;
            await _context.SaveChangesAsync();
        }
    }
}
