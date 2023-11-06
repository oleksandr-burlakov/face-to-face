using F2F.DLL.Entities;
using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class Assessment : BaseEntity
{
    public ICollection<Test> Tests { get; set; }
    public Guid? OneWayId { get; set; }
    public OneWay? OneWay { get; set; }
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public bool IsClosed { get; set; }
    public DateTime OpenDate { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
    public Guid? PreferableQuestionnaireId { get; set; }
    public Questionnaire? PreferableQuestionnaire { get; set; }
    public ICollection<AssessmentApply> Applies { get; set; }
}
