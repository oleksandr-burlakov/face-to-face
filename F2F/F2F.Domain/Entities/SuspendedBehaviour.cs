using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class SuspendedBehaviour : BaseEntity
{
    public Guid MeetingId { get; set; }
    public Meeting Meeting { get; set; }
    public DateTime Time { get; set; }
    public int BehiviourType { get; set; }
}
