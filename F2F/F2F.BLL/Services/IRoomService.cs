namespace F2F.BLL.Services;

public interface IRoomService
{
    public Task CloseRoom(Guid meetingId);
}
