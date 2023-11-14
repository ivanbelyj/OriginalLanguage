using Duende.IdentityServer.Models;

namespace OriginalLanguage.Identity.Configuration;

public class AppResources
{
    public static IEnumerable<ApiResource> Resources => new List<ApiResource>
    {
        new ApiResource("api")
    };
}
