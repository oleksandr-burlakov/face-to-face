using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F2F.BLL.Models.Questionnaire;
using FluentValidation;

namespace F2F.BLL.Models.Validators.Questionnaire;

public class AddQuestionnaireModelValidator : AbstractValidator<AddQuestionnaireModel>
{
    public AddQuestionnaireModelValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
    }
}
