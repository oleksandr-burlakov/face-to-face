using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F2F.BLL.Models.Question;
using FluentValidation;

namespace F2F.BLL.Models.Validators.Question;

public class AddQuestionModelValidator : AbstractValidator<AddQuestionModel>
{
    public AddQuestionModelValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
    }
}
