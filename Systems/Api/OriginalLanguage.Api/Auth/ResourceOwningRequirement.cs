using Microsoft.AspNetCore.Authorization;

namespace OriginalLanguage.Api.ResourceBasedAuth;

public class ResourceOwningRequirement : IAuthorizationRequirement
{
    public ResourceOwningRequirement()
    {

    }
}
