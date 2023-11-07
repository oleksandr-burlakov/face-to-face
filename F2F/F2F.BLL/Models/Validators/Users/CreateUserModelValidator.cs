using FluentValidation;
using Microsoft.AspNetCore.Identity;
using F2F.DLL.Entities;
using F2F.BLL.Models.Users;

namespace F2F.BLL.Models.Validators.Users;

public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    private readonly UserManager<User> _userManager;

    public CreateUserModelValidator(UserManager<User> userManager)
    {
        _userManager = userManager;

        RuleFor(u => u.Username)
            .MinimumLength(UserValidatorConfiguration.MinimumUsernameLength)
            .WithMessage(
                $"Username should have minimum {UserValidatorConfiguration.MinimumUsernameLength} characters"
            )
            .MaximumLength(UserValidatorConfiguration.MaximumUsernameLength)
            .WithMessage(
                $"Username should have maximum {UserValidatorConfiguration.MaximumUsernameLength} characters"
            )
            .Must(UsernameIsUnique)
            .WithMessage("Username is not available");

        RuleFor(u => u.Password)
            .MinimumLength(UserValidatorConfiguration.MinimumPasswordLength)
            .WithMessage(
                $"Password should have minimum {UserValidatorConfiguration.MinimumPasswordLength} characters"
            )
            .MaximumLength(UserValidatorConfiguration.MaximumPasswordLength)
            .WithMessage(
                $"Password should have maximum {UserValidatorConfiguration.MaximumPasswordLength} characters"
            );

        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("Email address is not valid")
            .Must(EmailAddressIsUnique)
            .WithMessage("Email address is already in use");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
    }

    private bool EmailAddressIsUnique(string email)
    {
        var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();

        return user == null;
    }

    private bool UsernameIsUnique(string username)
    {
        var user = _userManager.FindByNameAsync(username).GetAwaiter().GetResult();

        return user == null;
    }
}
