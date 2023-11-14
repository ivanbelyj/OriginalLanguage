using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context.Entities.User;

namespace OriginalLanguage.Services.UserAccount;
public class UserAccountService : IUserAccountService
{
    private readonly IModelValidator<RegisterUserAccountModel> registerModelValidator;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;
    public UserAccountService(UserManager<User> userManager,
        IModelValidator<RegisterUserAccountModel> validator,
        IMapper mapper)
    {
        this.userManager = userManager;
        registerModelValidator = validator;
        this.mapper = mapper;
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        registerModelValidator.Check(model);
        User? userWithEmail = await userManager.FindByEmailAsync(model.Email);
        if (userWithEmail != null)
        {
            throw new ProcessException($"User account with email {model.Email} already exist.");
        }

        User user = new User()
        {
            Status = UserStatus.Active,
            FullName = model.Name,
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true, // Todo: email confirmation
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
        };

        IdentityResult createRes = await userManager.CreateAsync(user, model.Password);
        if (!createRes.Succeeded)
            throw new ProcessException($"Creating user account is not succeeded. " +
                string.Join(", ", createRes.Errors.Select(s => s.Description)));

        return mapper.Map<UserAccountModel>(user);
    }
}
