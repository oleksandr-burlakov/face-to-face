using F2F.BLL.Models.Users;
using F2F.BLL.Models;
using F2F.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F2F.API.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IClaimService _claimService;

        public AccountController(IUserService userService, IClaimService claimService)
        {
            _userService = userService;
            _claimService = claimService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(CreateUserModel createUserModel)
        {
            return Ok(
                ApiResult<CreateUserResponseModel>.Success(
                    await _userService.CreateAsync(createUserModel)
                )
            );
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginUserModel loginUserModel)
        {
            return Ok(
                ApiResult<LoginResponseModel>.Success(await _userService.LoginAsync(loginUserModel))
            );
        }

        [Authorize]
        [HttpGet("get-info")]
        public async Task<IActionResult> GetInfo()
        {
            var id = _claimService.GetUserId();
            return Ok(
                ApiResult<GetInfoResponseModel>.Success(
                    await _userService.GetInfo(new GetInfoModel() { UserId = id })
                )
            );
        }
    }
}
