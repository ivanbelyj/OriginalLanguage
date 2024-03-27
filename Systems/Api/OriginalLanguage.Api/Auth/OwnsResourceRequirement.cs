using Microsoft.AspNetCore.Authorization;

namespace OriginalLanguage.Api.ResourceBasedAuth;

public class OwnsResourceRequirement : IAuthorizationRequirement
{
    public OwnsResourceRequirement()
    {

    }
}
