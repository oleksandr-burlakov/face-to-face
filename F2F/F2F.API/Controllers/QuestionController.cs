using F2F.BLL.Models;
using F2F.BLL.Models.Question;
using F2F.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F2F.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ApiController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [Authorize]
        [HttpGet("get-by-questionnaire/{questionnaireId}")]
        public async Task<IActionResult> GetByQuestionnaireAsync([FromRoute] Guid questionnaireId)
        {
            return Ok(
                ApiResult<IEnumerable<QuestionModel>>.Success(
                    await _questionService.GetByQuestionnaireIdAsync(questionnaireId)
                )
            );
        }

        [Authorize]
        [HttpPost("insert")]
        public async Task<IActionResult> InsertAsync([FromBody] AddQuestionModel model)
        {
            return Ok(ApiResult<Guid>.Success(await _questionService.InsertAsync(model)));
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateQuestionModel model)
        {
            await _questionService.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete("delete/{questionId}")]
        public async Task<IActionResult> DeleteAsync(Guid questionId)
        {
            await _questionService.DeleteAsync(questionId);
            return NoContent();
        }
    }
}
