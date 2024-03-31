using Microsoft.AspNetCore.Authorization;
using OriginalLanguage.Api.Helpers;
using OriginalLanguage.Context.Entities;
using System.Security.Claims;

namespace OriginalLanguage.Api.ResourceBasedAuth;

public class ResourceOwningHandler
    : AuthorizationHandler<ResourceOwningRequirement, string>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwningRequirement requirement,
        string resourceOwnerId)
    {
        var userId = UserUtils.GetUserIdClaim(context.User);

        if (userId == resourceOwnerId)
        {
            context.Succeed(requirement);
        } else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
