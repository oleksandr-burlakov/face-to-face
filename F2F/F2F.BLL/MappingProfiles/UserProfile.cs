using AutoMapper;
using F2F.BLL.Models.Users;
using F2F.DLL.Entities;

namespace F2F.BLL.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, User>();
    }
}
