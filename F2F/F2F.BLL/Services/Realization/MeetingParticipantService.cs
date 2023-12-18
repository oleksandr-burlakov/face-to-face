using F2F.DLL;
using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace F2F.BLL.Services.Realization;

public class MeetingParticipantService : IMeetingParticipantService
{
    private readonly F2FContext _context;

    public MeetingParticipantService(F2FContext context)
    {
        _context = context;
    }

    public async Task DeleteByParticipantIdAsync(string connectionId)
    {
        var participant = await _context.MeetingParticipants.FirstOrDefaultAsync(
            x => x.ParticipantId == connectionId
        );
        _context.MeetingParticipants.Remove(participant);
        await _context.SaveChangesAsync();
    }

    public async Task<MeetingParticipant> GetByConnectionId(string connectionId)
    {
        return await _context.MeetingParticipants.FirstOrDefaultAsync(
            x => x.ParticipantId == connectionId
        );
    }

    public async Task<IEnumerable<MeetingParticipant>> GetByMeetingIdAsync(Guid meetingId)
    {
        return await _context.MeetingParticipants
            .Where(x => x.MeetingId == meetingId)
            .ToListAsync();
    }

    public async Task InsertAsync(MeetingParticipant participant)
    {
        await _context.MeetingParticipants.AddAsync(participant);
        await _context.SaveChangesAsync();
    }
}
