using F2F.BLL.Models.Records;
using F2F.DLL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var directoryPath = "D:\\Projects\\Diploma\\F2F\\F2F.API\\StaticFiles";
            var filePath = Path.Combine(directoryPath, fileName);
            var fileMode = FileMode.Create;
            if (System.IO.File.Exists(filePath))
            {
                fileMode = FileMode.Append;
            }
            using (var fileStream = new FileStream(filePath, fileMode))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
            return Ok();
        }
    }
}