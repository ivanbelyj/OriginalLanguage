using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Auth;
using OriginalLanguage.Api.Helpers;
using OriginalLanguage.Api.ResourceBasedAuth;
using OriginalLanguage.Common.Utils;
using OriginalLanguage.Consts;

namespace OriginalLanguage.Api.Controllers;

public class AppControllerBase : ControllerBase
{
    private readonly IAuthorizationService authorizationService;
    public AppControllerBase(IAuthorizationService authorizationService)
    {
        this.authorizationService = authorizationService;
    }

    protected Guid? GetUserId()
    {
        return UserUtils.GetUserId(HttpContext.User);
    }

    /// <summary>
    /// Returns null if authorization is successful
    /// </summary>
    protected async Task<IActionResult?> ForbidIfResourceIsNotOwned(
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
