using F2F.BLL.Models.MeetingParticipant;
using F2F.DLL.Entities;

namespace F2F.BLL.Services;

public interface IMeetingParticipantService
{
    public Task<IEnumerable<MeetingParticipant>> GetByMeetingIdAsync(Guid meetingId);

    public Task InsertAsync(MeetingParticipant participant);

    public Task<MeetingParticipant> GetByConnectionId(string connectionId);

    public Task<DeleteParticipantResult> DeleteByParticipantIdAsync(string connectionId);
}
