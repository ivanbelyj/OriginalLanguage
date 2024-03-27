using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Auth;
using OriginalLanguage.Api.ResourceBasedAuth;
using OriginalLanguage.Consts;

namespace OriginalLanguage.Api.Controllers;

public class AppController : ControllerBase
{
    private readonly IAuthorizationService authorizationService;
    public AppController(IAuthorizationService authorizationService)
    {
        this.authorizationService = authorizationService;
    }

    /// <summary>
    /// Returns null if authorization is successful
    /// </summary>
    protected async Task<IActionResult?> ForbidNotOwnedResource(
        string resourceOwnerId)
    {
        var authResult = await authorizationService.AuthorizeAsync(
            User,
            resourceOwnerId,
            AuthConstants.OwnsResourcePolicy);

        if (!authResult.Succeeded)
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Forbid();
            }
            else
            {
                return Challenge();
            }
        }
        return null;
    }
}
