using F2F.BLL.Models.Users;
using F2F.BLL.Models;
using F2F.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using F2F.BLL.Models.Questionnaire;

namespace F2F.API.Controllers
{
    public class QuestionnaireController : ApiController
    {
        private readonly IClaimService _claimService;
        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(
            IQuestionnaireService questionnaireService,
            IClaimService claimService
        )
        {
            _questionnaireService = questionnaireService;
            _claimService = claimService;
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddAsync(AddQuestionnaireModel model)
        {
            model.AuthorId = _claimService.GetUserId();
            return Ok(ApiResult<Guid>.Success(await _questionnaireService.AddAsync(model)));
        }

        [Authorize]
        [HttpGet("get-my")]
        public async Task<IActionResult> GetMyAsync()
        {
            var id = _claimService.GetUserId();
            return Ok(
                ApiResult<IEnumerable<GetMyQuestionnairesResponseModel>>.Success(
                    await _questionnaireService.GetMyAsync(
                        new GetMyQuestionnairesModel() { AuthorId = id }
                    )
                )
            );
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateQuestionnaireModel model)
        {
            return Ok(ApiResult<Guid>.Success(await _questionnaireService.UpdateAsync(model)));
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _questionnaireService.DeleteAsync(id);
            return NoContent();
        }
    }
}
