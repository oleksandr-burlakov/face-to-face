using F2F.BLL.Models.Meetings;
using F2F.DLL.Entities;

namespace F2F.BLL.Services;

public interface IMeetingService
{
    public Task<Guid> InsertAsync(AddMeetingModel meeting);
    public Task UpdateAsync(UpdateMeetingModel meeting);
    public Task<IEnumerable<MeetingModel>> GetMyMeetings();
    public Task<MeetingModel> GetAsync(Guid id);
    public Task EndMeeting(Guid meetingId);
}
