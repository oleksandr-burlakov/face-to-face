using AutoMapper;
using F2F.BLL.Models.Meetings;
using F2F.DLL;
using F2F.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace F2F.BLL.Services.Realization;

public class MeetingService : IMeetingService
{
    private readonly F2FContext _context;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;

    public MeetingService(F2FContext context, IMapper mapper, IClaimService claimService)
    {
        _context = context;
        _mapper = mapper;
        _claimService = claimService;
    }

    public async Task<IEnumerable<MeetingModel>> GetMyMeetings()
    {
        var userId = _claimService.GetUserId();
        return _mapper.Map<IEnumerable<MeetingModel>>(
            await _context.Meetings.Where(x => x.OwnerId == userId).ToListAsync()
        );
    }

    public async Task<Guid> InsertAsync(AddMeetingModel model)
    {
        var meeting = _mapper.Map<Meeting>(model);
        meeting.OwnerId = _claimService.GetUserId();
        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync();
        return meeting.Id;
    }

    public async Task UpdateAsync(UpdateMeetingModel model)
    {
        var meeting = _mapper.Map<Meeting>(model);
        var dataFromDb = await _context.Meetings.FirstOrDefaultAsync(x => x.Id == meeting.Id);
        if (dataFromDb is not null)
        {
            dataFromDb.Title = meeting.Title;
            dataFromDb.ParticipantsEmail = meeting.ParticipantsEmail;
            dataFromDb.AssignedTime = meeting.AssignedTime;
            dataFromDb.AllowedConnectWithoutHost = meeting.AllowedConnectWithoutHost;
            dataFromDb.MaxAllowedParticipantsNumber = meeting.MaxAllowedParticipantsNumber;
            dataFromDb.SaveChat = meeting.SaveChat;
            dataFromDb.PreferableQuestionnaireId = meeting.PreferableQuestionnaireId;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<MeetingModel> GetAsync(Guid id)
    {
        return _mapper.Map<MeetingModel>(
            await _context.Meetings.FirstOrDefaultAsync(x => x.Id == id)
        );
    }

    public async Task EndMeeting(Guid meetingId)
    {
        var dataFromDb = await _context.Meetings.FirstOrDefaultAsync(x => x.Id == meetingId);
        if (dataFromDb is not null)
        {
            dataFromDb.IsFinished = true;
            await _context.SaveChangesAsync();
        }
    }
}
