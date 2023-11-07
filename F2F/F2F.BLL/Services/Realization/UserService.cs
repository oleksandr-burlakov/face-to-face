using AutoMapper;
using F2F.BLL.Exceptions;
using F2F.BLL.Helpers;
using F2F.BLL.Models.Users;
using F2F.DLL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace F2F.BLL.Services.Realization;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
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
}
