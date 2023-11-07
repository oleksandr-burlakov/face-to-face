using F2F.DLL.Entities.Base;

namespace F2F.DLL.Entities;

public class Questionnaire : BaseEntity
{
    public string Title { get; set; }
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public ICollection<Question> Questions { get; set; }
}
