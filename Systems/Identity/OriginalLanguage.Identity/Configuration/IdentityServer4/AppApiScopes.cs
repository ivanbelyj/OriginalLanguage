namespace OriginalLanguage.Identity.Configuration;
using Duende.IdentityServer.Models;
using OriginalLanguage.Common.Security;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.ArticlesRead,
                "Access to articles API - Read data"),
            new ApiScope(AppScopes.ArticlesWrite,
                "Access to articles API - Write data")
        };
}
