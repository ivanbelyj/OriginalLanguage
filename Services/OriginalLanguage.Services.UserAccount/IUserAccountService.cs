using OriginalLanguage.Services.UserAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.UserAccount;
public interface IUserAccountService
{
    Task<UserAccountModel> CreateUser(RegisterUserAccountModel model);
    Task UpdateUser(string id, UpdateUserAccountModel model);
    Task<UserAccountModel> GetUser(string userId);
    // Todo: password changing and recovery,
    // email confirmation
}
