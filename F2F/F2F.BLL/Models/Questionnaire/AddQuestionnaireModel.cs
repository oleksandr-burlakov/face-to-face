using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2F.BLL.Models.Questionnaire;

public class AddQuestionnaireModel
{
    public string Title { get; set; }
    public Guid AuthorId { get; set; }
}
