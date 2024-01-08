using System.Security.Claims;
using AutoMapper;
using F2F.BLL.Exceptions;
using F2F.BLL.Helpers;
using F2F.BLL.Models.Users;
using F2F.DLL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace F2F.BLL.Services.Realization;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(
        IMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration
    )
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel)
    {
        var user = _mapper.Map<User>(createUserModel);

        var result = await _userManager.CreateAsync(user, createUserModel.Password);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        return new CreateUserResponseModel
        {
            Id = (await _userManager.FindByNameAsync(user.UserName)).Id
        };
    }

    public async Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(
            u => u.UserName == loginUserModel.Username
        );

        if (user == null)
            throw new NotFoundException("Username or password is incorrect");

        var passwordCheckResult = await _userManager.CheckPasswordAsync(
            user,
            loginUserModel.Password
        );

        if (!passwordCheckResult)
            throw new BadRequestException("Username or password is incorrect");

        var token = JwtHelper.GenerateToken(user, _configuration);

        return new LoginResponseModel
        {
            Username = user.UserName,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<GetInfoResponseModel> GetInfo(GetInfoModel getInfoModel)
    {
        var user = await _userManager.FindByIdAsync(getInfoModel.UserId.ToString());
        var roles = await _userManager.GetRolesAsync(user);
        var result = _mapper.Map<GetInfoResponseModel>(user);
        result.Role = roles.FirstOrDefault();
        return result;
    }

    public async Task<LoginResponseModel> LoginByExternal()
    {
        ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

        var signinResult = await _signInManager.ExternalLoginSignInAsync(
            info.LoginProvider,
            info.ProviderKey,
            false
        );
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.FindByEmailAsync(email);

        if (!signinResult.Succeeded && String.IsNullOrWhiteSpace(email))
        {
            throw new BadRequestException("It's not possible to log in");
        }
        else if (!signinResult.Succeeded && !String.IsNullOrWhiteSpace(email))
        {
            if (user == null)
            {
                user = new User()
                {
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                };
                await _userManager.CreateAsync(user);
            }

            await _userManager.AddLoginAsync(user, info);
        }

        var identity = new ClaimsIdentity(
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimTypes.Name,
            ClaimTypes.Role
        );
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        foreach (var role in _userManager.GetRolesAsync(user).Result)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        AuthenticationProperties _authentication = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow,
        };

        await _signInManager.SignInAsync(user, _authentication);
        var jwtResult = JwtHelper.GenerateToken(user, _configuration);

        //sucess

        return new LoginResponseModel
        {
            Username = user.UserName,
            Email = user.Email,
            Token = jwtResult
        };
    }
}
