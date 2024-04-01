using System.Security.Claims;

namespace OriginalLanguage.Common.Utils;

public static class UserUtils
{
    public static string? GetUserIdClaimValue(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public static Guid? GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        Guid.TryParse(GetUserIdClaimValue(claimsPrincipal), out Guid res);
        return res;
    }
}
