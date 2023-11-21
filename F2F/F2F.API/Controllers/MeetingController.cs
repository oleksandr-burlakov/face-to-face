using F2F.BLL.Models;
using F2F.BLL.Models.Meetings;
using F2F.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F2F.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ApiController
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [Authorize]
        [HttpGet("get-my")]
        public async Task<IActionResult> GetMyMeetings()
        {
            return Ok(
                ApiResult<IEnumerable<MeetingModel>>.Success(await _meetingService.GetMyMeetings())
            );
        }

        [Authorize]
        [HttpPost("insert")]
        public async Task<IActionResult> InsertAsync([FromBody] AddMeetingModel model)
        {
            return Ok(ApiResult<Guid>.Success(await _meetingService.InsertAsync(model)));
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateMeetingModel model)
        {
            await _meetingService.UpdateAsync(model);
            return Ok();
        }
    }
}
