using F2F.BLL.Models;
using F2F.BLL.Models.Users;
using F2F.BLL.Services;
using F2F.DLL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Web;

namespace F2F.API.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IClaimService _claimService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            IUserService userService,
            IClaimService claimService,
            SignInManager<User> signInManager
        )
        {
            _userService = userService;
            _claimService = claimService;
            _signInManager = signInManager;
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

        [HttpGet]
        [AllowAnonymous]
        [Route("external-login")]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            returnUrl = HttpUtility.UrlEncode(returnUrl);
            var redirectUrl =
                $"https://localhost:7243/api/Account/external-auth-callback?returnUrl={returnUrl}";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                provider,
                redirectUrl
            );
            properties.AllowRefresh = true;
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("external-auth-callback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var result = ApiResult<LoginResponseModel>.Success(
                await _userService.LoginByExternal()
            );
            if (result.Succeeded)
            {
                HttpContext.Response.Cookies.Append(
                    "token",
                    JsonConvert.SerializeObject(result.Result)
                );
            }
            return Redirect(returnUrl);
        }
    }
}
