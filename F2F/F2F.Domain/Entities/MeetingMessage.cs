using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class MeetingMessage : BaseEntity
{
    public Guid MeetingId { get; set; }
    public Meeting Meeting { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public DateTime SendTime { get; set; }
}
