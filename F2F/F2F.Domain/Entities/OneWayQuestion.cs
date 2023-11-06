using F2F.DLL.Entities.Base;

namespace F2F.Domain.Entities;

public class OneWayQuestion : BaseEntity
{
    public Guid OneWayId { get; set; }
    public OneWay OneWay { get; set; }
    public int NumberOfAttempts { get; set; }
    public string Content { get; set; }
    public TimeSpan TimeForAnswer { get; set; }
}
