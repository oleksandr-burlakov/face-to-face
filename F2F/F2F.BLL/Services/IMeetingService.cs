using F2F.BLL.Models.Meetings;
using F2F.DLL.Entities;

namespace F2F.BLL.Services;

public interface IMeetingService
{
    public Task<Guid> InsertAsync(Meeting meeting);
    public Task UpdateAsync(Meeting meeting);
    public Task<IEnumerable<MeetingModel>> GetMyMeetings(Guid userId);
}
