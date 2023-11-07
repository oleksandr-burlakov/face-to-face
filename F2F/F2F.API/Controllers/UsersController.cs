using F2F.BLL.Models.Users;
using F2F.BLL.Models;
using F2F.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F2F.API.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
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
    }
}
