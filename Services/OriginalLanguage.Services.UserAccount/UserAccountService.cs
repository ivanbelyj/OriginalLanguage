using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context.Entities.User;

namespace OriginalLanguage.Services.UserAccount;
public class UserAccountService : IUserAccountService
{
    private readonly IModelValidator<RegisterUserAccountModel> registerModelValidator;
    private readonly UserManager<AppUser> userManager;
    private readonly IMapper mapper;
    //private readonly ILogger logger;
    public UserAccountService(UserManager<AppUser> userManager,
        IModelValidator<RegisterUserAccountModel> validator,
        IMapper mapper)
    {
        this.userManager = userManager;
        registerModelValidator = validator;
        this.mapper = mapper;
        //this.logger = logger;
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        //logger.LogDebug($"Creating account: {model.Email} {model.Name}");
        registerModelValidator.Check(model);
        AppUser? userWithEmail = await userManager.FindByEmailAsync(model.Email);
        if (userWithEmail != null)
        {
            throw new ProcessException($"User account with email {model.Email} already exist.");
        }

        AppUser user = new AppUser()
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
