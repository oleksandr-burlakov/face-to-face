using F2F.BLL.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace F2F.BLL.Services;

public interface IUserService
{
    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);

    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
    Task<GetInfoResponseModel> GetInfo(GetInfoModel getInfoModel);
    Task<LoginResponseModel> LoginByExternal();
}
