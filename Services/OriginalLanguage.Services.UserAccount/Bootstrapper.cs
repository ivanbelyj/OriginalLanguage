using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.UserAccount;
public static class Bootstrapper
{
    public static IServiceCollection AddUserAccountService(
        this IServiceCollection services)
    {
        services.AddScoped<IUserAccountService, UserAccountService>();
        return services;
    }
}
