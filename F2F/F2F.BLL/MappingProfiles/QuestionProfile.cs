using AutoMapper;
using F2F.BLL.Models.Question;
using F2F.DLL.Entities;

namespace F2F.BLL.MappingProfiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<AddQuestionModel, Question>();
        CreateMap<UpdateQuestionModel, Question>();
        CreateMap<Question, QuestionModel>();
    }
}
