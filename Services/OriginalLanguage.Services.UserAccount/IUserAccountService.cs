using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.UserAccount;
public interface IUserAccountService
{
    Task<UserAccountModel> Create(RegisterUserAccountModel model);
    // Todo: account data editing, password changing and recovery,
    // email confirmation
}
