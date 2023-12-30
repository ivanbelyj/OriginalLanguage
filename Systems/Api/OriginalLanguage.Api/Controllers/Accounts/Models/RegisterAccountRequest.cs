using AutoMapper;
using FluentValidation;
using OriginalLanguage.Services.UserAccount;
using OriginalLanguage.Services.UserAccount.Models;

namespace OriginalLanguage.Api.Controllers.Accounts;

public class RegisterAccountRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}


public class RegisterAccountRequestValidator : AbstractValidator<RegisterAccountRequest>
{
    public RegisterAccountRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is too long.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}

public class RegisterAccountRequestProfile : Profile
{
    public RegisterAccountRequestProfile()
    {
        CreateMap<RegisterAccountRequest, RegisterUserAccountModel>();
    }
}
