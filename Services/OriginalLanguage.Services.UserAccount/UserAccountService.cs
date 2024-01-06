using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Services.Actions;
using OriginalLanguage.Services.EmailSender;
using OriginalLanguage.Services.EmailSender.Models;
using OriginalLanguage.Services.UserAccount.Models;
using OriginalLanguage.Context.Entities;

namespace OriginalLanguage.Services.UserAccount;
public class UserAccountService : IUserAccountService
{
    private readonly IModelValidator<RegisterUserAccountModel> registerValidator;
    private readonly IModelValidator<UpdateUserAccountModel> updateModelValidator;
    private readonly UserManager<AppUser> userManager;
    private readonly IMapper mapper;
    private readonly ISendEmailAction sendEmailAction;

    private readonly ILogger logger;
    private readonly SignInManager<AppUser> signInManager;

    public UserAccountService(UserManager<AppUser> userManager,
        IModelValidator<RegisterUserAccountModel> registerValidator,
        IModelValidator<UpdateUserAccountModel> updateModelValidator,
        IMapper mapper,
        ISendEmailAction sendEmailAction,
        ILogger logger,
        SignInManager<AppUser> signInManager
        )
    {
        this.userManager = userManager;
        this.registerValidator = registerValidator;
        this.updateModelValidator = updateModelValidator;
        this.mapper = mapper;
        this.sendEmailAction = sendEmailAction;
        this.logger = logger;
        this.signInManager = signInManager;
    }

    public async Task<UserAccountModel> CreateUser(RegisterUserAccountModel model)
    {
        logger.Information($"Creating account: {model.Email} {model.Name}");
        registerValidator.Check(model);
        AppUser? userWithEmail = await userManager.FindByEmailAsync(model.Email);
        if (userWithEmail != null)
        {
            throw new ProcessException($"User account with email {model.Email} " +
                $"already exist.");
        }

        AppUser user = new AppUser()
        {
            Status = UserStatus.Active,
            UserName = model.Name,
            Email = model.Email,
            EmailConfirmed = true, // Todo: email confirmation
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
        };

        await sendEmailAction.SendEmail(new EmailModel
        {
            Content = $"Test email for user {user.Email}"
        });

        IdentityResult createRes = await userManager.CreateAsync(user, model.Password);
        if (!createRes.Succeeded)
            throw new ProcessException($"Creating user account is not succeeded. " +
                string.Join(", ", createRes.Errors.Select(s => s.Description)));

        return mapper.Map<UserAccountModel>(user);
    }

    public async Task UpdateUser(string id, UpdateUserAccountModel model)
    {
        updateModelValidator.Check(model);

        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new ProcessException($"User (id: {id}) was not found.");
        }

        var updateResult = await userManager.UpdateAsync(mapper.Map(model, user));
        if (!updateResult.Succeeded)
        {
            throw new ProcessException($"Updating user profile is not succeeded. " +
                string.Join(", ", updateResult.Errors.Select(s => s.Description)));
        }
    }

    public async Task<UserAccountModel> GetUser(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ProcessException($"User (id: {userId}) was not found.");
        }

        return mapper.Map<UserAccountModel>(user);
    }

    public async Task DeleteUser(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ProcessException($"User (id: {userId}) does not exist.");
        }

        var deleteResult = await userManager.DeleteAsync(user);
        if (!deleteResult.Succeeded)
        {
            throw new ProcessException($"Deleting user is not succeeded. " +
                string.Join(", ", deleteResult.Errors.Select(s => s.Description)));
        }
    }

    public async Task ChangePassword(string userId,
        string oldPassword, string newPassword)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ProcessException($"User (id: {userId}) was not found.");
        }

        var checkPasswordResult = await userManager.CheckPasswordAsync(user,
            oldPassword);
        if (!checkPasswordResult)
        {
            throw new ProcessException("Old password is incorrect.");
        }

        var changePasswordResult = await userManager.ChangePasswordAsync(user,
            oldPassword, newPassword);
        if (!changePasswordResult.Succeeded)
        {
            throw new ProcessException($"Changing password is not succeeded. " +
                string.Join(", ", changePasswordResult.Errors.Select(s => s.Description)));
        }
    }

    //public async Task RecoverPassword(string email)
    //{
    //    var user = await userManager.FindByEmailAsync(email);
    //    if (user == null)
    //    {
    //        throw new ProcessException($"User (email: {email}) does not exist.");
    //    }

    //    var token = await userManager.GeneratePasswordResetTokenAsync(user);
    //    var passwordResetLink = UrlHelper.Action("ResetPassword",
    //        "Account", new { token = token }, Request.Scheme);

    //    await sendEmailAction.SendEmail(new EmailModel
    //    {
    //        To = email,
    //        Subject = "Password Reset",
    //        Content = $"Click here to reset your password: {passwordResetLink}"
    //    });
    //}
}
