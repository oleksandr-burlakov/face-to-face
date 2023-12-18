using F2F.BLL.Models.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
            using (var fs = new FileStream(fileName, fileMode, FileAccess.Write))
            {
                await fs.WriteAsync(bytes, 0, bytes.Length);
            }
            return Ok();
        }
    }
}
