using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.UserAccount;
using OriginalLanguage.Services.UserAccount.Models;

namespace OriginalLanguage.Api.Controllers.Accounts;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/accounts")]
[ApiController]
[ApiVersion("1.0")]
public class AccountsController : ControllerBase
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
            .CreateUser(mapper.Map<RegisterUserAccountModel>(request));
        var response = mapper.Map<UserAccountResponse>(userAccountModel);
        return response;
    }
}
