using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Services.UserAccount;

namespace OriginalLanguage.Api.Controllers.Accounts;

[Route("api/v{version:apiVersion}/accounts")]
[ApiController]
[ApiVersion("1.0")]
public class AccountsController : Controller
{
    private readonly IUserAccountService userAccountService;
    private readonly IMapper mapper;
    public AccountsController(IUserAccountService userAccountService, IMapper mapper)
    {
        this.userAccountService = userAccountService;
        this.mapper = mapper;
    }

    [HttpPost("")]
    public async Task<UserAccountResponse> Register(
        [FromBody] RegisterAccountRequest request)
    {
        UserAccountModel userAccountModel = await userAccountService
            .Create(mapper.Map<RegisterUserAccountModel>(request));
        var response = mapper.Map<UserAccountResponse>(userAccountModel);
        return response;
    }
}
