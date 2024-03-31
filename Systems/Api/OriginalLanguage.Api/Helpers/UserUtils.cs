using System.Security.Claims;

namespace OriginalLanguage.Api.Helpers;

public static class UserUtils
{
    public static string? GetUserIdClaim(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static Guid? GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        Guid.TryParse(GetUserIdClaim(claimsPrincipal), out Guid res);
        return res;
    }
}
