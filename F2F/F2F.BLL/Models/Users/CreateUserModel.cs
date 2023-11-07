﻿namespace F2F.BLL.Models.Users;

public class CreateUserModel
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateUserResponseModel : BaseResponseModel { }
