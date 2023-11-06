using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class OneWay : BaseEntity
{
    public string Title { get; set; }
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public TimeSpan AllowedTime { get; set; }
    public ICollection<OneWayQuestion> OneWayQuestions { get; set; }
    public ICollection<OneWayAttempt> OneWayAttempts { get; set; }
}
