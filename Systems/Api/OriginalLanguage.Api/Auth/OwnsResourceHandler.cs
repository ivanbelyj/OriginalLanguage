using Microsoft.AspNetCore.Authorization;
using OriginalLanguage.Context.Entities;
using System.Security.Claims;

namespace OriginalLanguage.Api.ResourceBasedAuth;

public class OwnsResourceHandler
    : AuthorizationHandler<OwnsResourceRequirement, string>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnsResourceRequirement requirement,
        string resourceOwnerId)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

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
