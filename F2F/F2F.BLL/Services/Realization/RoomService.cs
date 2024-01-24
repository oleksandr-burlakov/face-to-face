using F2F.DLL;
using Microsoft.EntityFrameworkCore;
using NReco.VideoConverter;

namespace F2F.BLL.Services.Realization;

internal class RoomService : IRoomService
{
    private readonly F2FContext _context;

    public RoomService(F2FContext context)
    {
        _context = context;
    }

    public async Task CloseRoom(Guid meetingId)
    {
        var meeting = await _context.Meetings.FirstOrDefaultAsync(x => x.Id == meetingId);
        if (meeting != null)
        {
            meeting.IsFinished = true;
            await _context.SaveChangesAsync();
        }
        var directoryPath = "D:\\Projects\\Diploma\\F2F\\F2F.API\\StaticFiles";
        var fileName = $"test-{meetingId}.webm";
        var newFileName = $"{fileName.Split(".")[0]}.mp4";
        var filePath = Path.Combine(directoryPath, fileName);
        var newFilePath = Path.Combine(directoryPath, newFileName);
        if (File.Exists(filePath))
        {
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.ConvertMedia(filePath, newFilePath, Format.mp4);
            System.IO.File.Delete(filePath);
            meeting.RecordLink = newFileName;
            await _context.SaveChangesAsync();
        }
    }
}
