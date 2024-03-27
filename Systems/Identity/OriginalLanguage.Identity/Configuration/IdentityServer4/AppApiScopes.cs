namespace OriginalLanguage.Identity.Configuration;
using Duende.IdentityServer.Models;
using OriginalLanguage.Consts;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.ContentWrite,
                "Access to API - Write content"),
            new ApiScope(AppScopes.CoursesLearn,
                "Access to API - Read courses and write learning data")
        };
}
