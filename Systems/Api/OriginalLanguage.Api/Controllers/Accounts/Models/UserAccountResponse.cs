using AutoMapper;
using OriginalLanguage.Services.UserAccount;

namespace OriginalLanguage.Api.Controllers.Accounts;

public class UserAccountResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserAccountResponseProfile : Profile
{
    public UserAccountResponseProfile()
    {
        CreateMap<UserAccountModel, UserAccountResponse>();
    }
}
