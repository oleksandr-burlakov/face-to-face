using AutoMapper;
using F2F.BLL.Models.Questionnaire;
using F2F.DLL.Entities;

namespace F2F.BLL.MappingProfiles;

public class QuestionnaireProfile : Profile
{
    public QuestionnaireProfile()
    {
        CreateMap<AddQuestionnaireModel, Questionnaire>();
        CreateMap<UpdateQuestionnaireModel, Questionnaire>();
        CreateMap<Questionnaire, GetMyQuestionnairesResponseModel>();
    }
}
