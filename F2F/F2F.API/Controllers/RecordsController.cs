using F2F.BLL.Models.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NReco.VideoConverter;

namespace F2F.API.Controllers
{
    public class RecordsController : ApiController
    {
        [HttpPost("send-data")]
        [AllowAnonymous]
        public async Task<IActionResult> SendData([FromBody] SendDataRequest model)
        {
            var bytes = Convert.FromBase64String(model.Blob);
            var fileName = $"test-{model.MeetingId}.webm";
            var fileMode = FileMode.Create;
            if (System.IO.File.Exists(fileName))
            {
                fileMode = FileMode.Append;
            }
            using (var fileStream = new FileStream(fileName, fileMode))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
            return Ok();
        }

        [HttpPost("end-record")]
        [AllowAnonymous]
        public async Task<IActionResult> EndRecord(
            [FromBody] EndRecordRequest model,
            CancellationToken cancellationToken
        )
        {
            var fileName = $"test-{model.MeetingId}.webm";
            if (!System.IO.File.Exists(fileName))
            {
                return BadRequest("File doesn't exists");
            }
            var newFileName = $"{fileName.Split(".")[0]}.mp4";
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.ConvertMedia(fileName, newFileName, Format.mp4);
            System.IO.File.Delete(fileName);
            return Ok();
        }
    }
}
