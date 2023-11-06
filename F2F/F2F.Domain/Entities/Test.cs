using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class Test : BaseEntity
{
    public string Title { get; set; }
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public int ShowByType { get; set; }
    public int NumberOfAllowedAttempts { get; set; }
    public TimeSpan TimeBoundaries { get; set; }
    public int? NumberOfItemPerPage { get; set; }
    public bool AllowedToSeeAnswersBeforeReview { get; set; }
    public ICollection<TestSection> TestSections { get; set; }
    public ICollection<TestAttempt> TestAttempts { get; set; }
    public Guid AssessmentId { get; set; }
    public Assessment Assessment { get; set; }
}
